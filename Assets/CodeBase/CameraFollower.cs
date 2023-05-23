using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase
{
    public class CameraFollower : MonoCache
    {
        private Transform _target;
        private Transform _sefTransform;

        private void Awake() =>
            _sefTransform = transform;

        protected override void LateRun() =>
            _sefTransform.position = _target.position;

        public void Follow(Transform hero) =>
            _target = hero;
    }
}