using CodeBase.Logic.Observer;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class ItemAttractor : ObserverTarget<IAttractable, TriggerObserver>
    {
        [SerializeField] private float _attractDistance;
        [SerializeField] private SphereCollider _attractCollider;

        private void Start()
        {
            _attractCollider ??= GetComponent<SphereCollider>();
            SetAttractDistance(_attractDistance);
        }

        private void OnValidate()
        {
            if (Application.isEditor)
            {
                SetAttractDistance(_attractDistance);
            }
        }

        private void SetAttractDistance(float distance) =>
            _attractCollider.radius = distance;

        protected override void OnTargetEntered(IAttractable attractable) =>
            attractable.Attract(transform);
    }
}