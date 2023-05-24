using System;
using CodeBase.Logic.Observer;
using CodeBase.Logic.Player;
using CodeBase.Logic.Wallet;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Interactions
{
    [RequireComponent(typeof(TimerOperator))]
    public class InteractionPaidZone : ObserverTargetExit<HeroWallet, TriggerObserverExit>
    {
        [SerializeField] private int _interactionCost = 60;
        [SerializeField] private int _coinsPerTick = 1;
        [SerializeField] private float _tickDuration = 0.1f;

        private int _interactionCostLeft;
        private TimerOperator _timerOperator;
        private ISpend _wallet;

        public event Action FullPaid = () => { };

        private void Start()
        {
            _timerOperator ??= GetComponent<TimerOperator>();
            _timerOperator.SetUp(_tickDuration, OnTick);
            _interactionCostLeft = _interactionCost;
        }

        private void OnTick()
        {
            if (_interactionCostLeft <= 0)
            {
                FullPaid.Invoke();
                gameObject.Disable();
                return;
            }

            if (_wallet.TrySpend(_coinsPerTick))
            {
                _interactionCostLeft -= _coinsPerTick;
                _timerOperator.Restart();
            }
        }

        protected override void OnTargetEntered(HeroWallet target)
        {
            _wallet = target.Wallet;
            _timerOperator.Restart();
        }

        protected override void OnTargetExited(HeroWallet target) =>
            _timerOperator.Pause();
    }
}