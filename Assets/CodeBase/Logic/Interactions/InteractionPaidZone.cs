using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Items;
using CodeBase.Logic.Observer;
using CodeBase.Logic.Wallet;
using NaughtyAttributes;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Interactions
{
    [RequireComponent(typeof(TimerOperator))]
    public abstract class InteractionPaidZone<T> : ObserverTargetExit<T, TriggerObserverExit> where T : MonoBehaviour
    {
        [Dropdown("CurrencyNames")] [field: SerializeField] private string _currencyName;
        [SerializeField] private int _interactionCost = 60;
        [SerializeField] private int _coinsPerTick = 1;
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

            FindTypes();
            SetChosenType();
        }

        private void OnValidate()
        {
            FindTypes();
            SetChosenType();
        }

        protected abstract void SetSpendable(ref ISpend spendable, T target);

        protected override void OnTargetEntered(T target)
        {
            SetSpendable(ref _spendable, target);
            _timerOperator.Restart();
        }

        protected override void OnTargetExited(T target) =>
            _timerOperator.Pause();

        protected abstract bool TrySpend(ISpend spendable, Type itemType, int amount);

        private void OnTick()
        {
            if (_interactionCostLeft <= 0)
            {
                FullPaid.Invoke();
                gameObject.Disable();
                return;
            }

            if (TrySpend(_spendable, _currencyType, _coinsPerTick))
            {
                _interactionCostLeft -= _coinsPerTick;
                CostChanged.Invoke(_interactionCostLeft);
                _timerOperator.Restart();
            }
        }

        private void SetChosenType() =>
            _currencyType = _currencyTypes.Find(type => string.Equals(type.Name, _currencyName));

        private void FindTypes() => _currencyTypes = typeof(IItem).Assembly.ExportedTypes
            .Where(t => typeof(IItem).IsAssignableFrom(t) && t.IsInterface == false).ToList();
    }
}