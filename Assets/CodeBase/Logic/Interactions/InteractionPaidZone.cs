using System;
using CodeBase.Logic.Observer;
using CodeBase.Logic.Wallet;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Interactions
{
    [RequireComponent(typeof(TimerOperator))]
    public abstract class InteractionPaidZone<T> : ObserverTargetExit<T, TriggerObserverExit> where T : MonoBehaviour
    {
        [SerializeField] private int _interactionCost = 60;
        [SerializeField] private int _coinsPerTick = 1;
        [SerializeField] private float _tickDuration = 0.1f;

        private int _interactionCostLeft;
        private TimerOperator _timerOperator;
        private ISpend _spendable;

        public event Action FullPaid = () => { };
        public event Action<int> CostChanged = i => { };

        public int InteractionCost => _interactionCost;

        protected virtual void Start()
        {
            _timerOperator ??= GetComponent<TimerOperator>();
            _timerOperator.SetUp(_tickDuration, OnTick);
            _interactionCostLeft = _interactionCost;
        }

        protected abstract void SetSpendable(ref ISpend spendable, T target);

        protected override void OnTargetEntered(T target)
        {
            SetSpendable(ref _spendable, target);
            _timerOperator.Restart();
        }

        protected override void OnTargetExited(T target) =>
            _timerOperator.Pause();

        protected abstract bool TrySpend(ISpend spendable, int amount);

        private void OnTick()
        {
            if (_interactionCostLeft <= 0)
            {
                FullPaid.Invoke();
                gameObject.Disable();
                return;
            }

            if (TrySpend(_spendable, _coinsPerTick))
            {
                _interactionCostLeft -= _coinsPerTick;
                CostChanged.Invoke(_interactionCostLeft);
                _timerOperator.Restart();
            }
        }
    }
}