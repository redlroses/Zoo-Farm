using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    [RequireComponent(typeof(TriggerObserver))]
    public abstract class ObserverTarget<TTarget, TObserver> : MonoCache where TObserver : ITriggerObserverCommon
    {
        protected TObserver Observer;

        private void Awake() =>
            GetObserver();

        protected override void OnEnabled() =>
            Observer.Entered += OnTriggerObserverEntered;

        protected override void OnDisabled() =>
            Observer.Entered -= OnTriggerObserverEntered;

        protected abstract void OnTargetEntered(TTarget _);

        protected virtual void GetObserver() =>
            Observer ??= GetComponent<TObserver>();

        private void OnTriggerObserverEntered(Collider other)
        {
            if (other.TryGetComponent(out TTarget target))
            {
                OnTargetEntered(target);
            }
        }
    }
}