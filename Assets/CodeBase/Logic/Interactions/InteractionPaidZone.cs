﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Inventory;
using CodeBase.Logic.Items;
using CodeBase.Logic.Observer;
using CodeBase.Tools;
using NaughtyAttributes;
using NTC.Global.System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.Interactions
{
    [RequireComponent(typeof(TimerOperator))]
    public abstract class InteractionPaidZone<T> : ObserverTargetExit<T, TriggerObserverExit> where T : MonoBehaviour
    {
        [Dropdown("CurrencyNames")] [field: SerializeField] private string _currencyName;
        [SerializeField] private int _interactionCost = 60;
        [FormerlySerializedAs("_coinsPerTick")] [SerializeField] private int _currencyPerTick = 1;
        [SerializeField] private float _tickDuration = 0.1f;

        private int _interactionCostLeft;
        private TimerOperator _timerOperator;
        private ISpend _spendable;
        private List<Type> _currencyTypes;
        private Type _currencyType;

        public event Action FullPaid = () => { };
        public event Action<int> CostChanged = i => { };

        public int InteractionCost => _interactionCost;

        // ReSharper disable once UnusedMember.Local
        private String[] CurrencyNames => _currencyTypes.Select(type => type.Name).ToArray();

        protected virtual void Start()
        {
            _timerOperator ??= GetComponent<TimerOperator>();
            _timerOperator.SetUp(_tickDuration, OnTick);
            _interactionCostLeft = _interactionCost;

            SetFoundTypes();
        }

        private void SetFoundTypes()
        {
            TypeFinder typeFinder = new TypeFinder(typeof(IItem));
            _currencyTypes = typeFinder.GetTypes();
            _currencyType = typeFinder.GetTypeByName(_currencyName);
        }

        private void OnValidate() =>
            SetFoundTypes();

        protected abstract void SetSpendable(ref ISpend spendable, T target);

        protected abstract bool TrySpend(ISpend spendable, Type itemType, int amount);

        protected override void OnTargetEntered(T target)
        {
            SetSpendable(ref _spendable, target);
            _timerOperator.Restart();
        }

        protected override void OnTargetExited(T target) =>
            _timerOperator.Pause();

        private void OnTick()
        {
            if (_interactionCostLeft <= 0)
            {
                FullPaid.Invoke();
                gameObject.Disable();
                return;
            }

            if (TrySpend(_spendable, _currencyType, _currencyPerTick))
            {
                _interactionCostLeft -= _currencyPerTick;
                CostChanged.Invoke(_interactionCostLeft);
                _timerOperator.Restart();
            }
        }
    }
}