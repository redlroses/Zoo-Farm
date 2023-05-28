using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Observer;
using CodeBase.Tools;
using NaughtyAttributes;
using UnityEngine;

namespace CodeBase.Logic.Attractor
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class ItemAttractor : ObserverTarget<IAttractable, TriggerObserver>
    {
        [Dropdown("AttractNames")] [field: SerializeField] private string _attractName;

        [SerializeField] private float _attractDistance;
        [SerializeField] private SphereCollider _attractCollider;

        private List<Type> _attractTypes;
        private Type _attractType;

        private String[] AttractNames => _attractTypes.Select(type => type.Name).ToArray();

        private void Start()
        {
            SetFoundTypes();
            _attractCollider ??= GetComponent<SphereCollider>();
            SetAttractDistance(_attractDistance);
        }

        private void OnValidate()
        {
            if (Application.isEditor)
            {
                _attractCollider ??= GetComponent<SphereCollider>();
                SetFoundTypes();
                SetAttractDistance(_attractDistance);
            }
        }

        private void SetAttractDistance(float distance) =>
            _attractCollider.radius = distance;

        protected override void OnTargetEntered(IAttractable attractable)
        {
            if (attractable.GetType() == _attractType)
                attractable.Attract(transform);
        }

        private void SetFoundTypes()
        {
            TypeFinder typeFinder = new TypeFinder(typeof(IAttractable));
            _attractTypes = typeFinder.GetTypes();
            _attractType = typeFinder.GetTypeByName(_attractName);
        }
    }
}