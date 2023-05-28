using System;
using NTC.Global.Cache;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Attractor
{
    public class Attracted : MonoCache, IAttractable
    {
        [SerializeField] private float _attractSpeed;
        [SerializeField] private AnimationCurve _sizeCurve;
        [SerializeField] private float _onAttractedDistance;

        private Transform _selfTransform;
        private Transform _toTransform;
        private float _startDistance;
        private Vector3 _startScale;
        private Vector3 _defaultScale;

        public event Action<IAttractable> WasAttracted = c => { };

        public GameObject GameObject => gameObject;

        private void Awake()
        {
            enabled = false;
            _selfTransform = transform;
            _toTransform = _selfTransform;
            _defaultScale = _selfTransform.lossyScale;
        }

        protected override void OnDisabled() =>
            WasAttracted.Invoke(this);

        public void Attract(Transform to)
        {
            _toTransform = to;
            _startDistance = Vector3.Distance(_toTransform.position, _selfTransform.position);
            _startScale = _defaultScale;
            enabled = true;
        }

        protected override void Run()
        {
            Vector3 toTargetVector = _toTransform.position - _selfTransform.position;
            float distance = toTargetVector.magnitude;
            _selfTransform.Translate(toTargetVector.normalized * (_attractSpeed * Time.deltaTime), Space.World);
            ScaleSize(distance);

            if (distance <= _onAttractedDistance)
            {
                OnAttracted();
                enabled = false;
                gameObject.Disable();
                _selfTransform.localScale = _startScale;
            }
        }

        protected virtual void OnAttracted() { }

        private void ScaleSize(float byDistance)
        {
            float normalizedDistance = Mathf.InverseLerp(_startDistance, 0, byDistance);
            float scaleFactor = _sizeCurve.Evaluate(normalizedDistance);
            Vector3 scaledVector = Vector3.one * scaleFactor;
            Vector3 scaledScale = Vector3.Scale(_startScale, scaledVector);
            _selfTransform.localScale = scaledScale;
        }
    }
}