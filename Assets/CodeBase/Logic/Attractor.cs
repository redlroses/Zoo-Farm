using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Attractor : MonoCache, IAttractable
    {
        [SerializeField] private float _attractSpeed;

        private Transform _selfTransform;
        private Transform _toTransform;

        private void Awake()
        {
            enabled = false;
            _selfTransform = transform;
            _toTransform = _selfTransform;
        }

        public void Attract(Transform to)
        {
            _toTransform = to;
            enabled = true;
        }

        protected override void Run() =>
            _selfTransform.Translate((_toTransform.position - _selfTransform.position).normalized * (_attractSpeed * Time.deltaTime));
    }
}