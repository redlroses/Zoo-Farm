using System;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CodeBase.UI.Elements
{
    class TextSetterAnimated : TextSetter
    {
        [SerializeField] private AnimationCurve _curveAnimation;
        [SerializeField] private float _duration;

#if UNITY_EDITOR
        [SerializeField] private int _testNumber;
#endif

        private float _elapsedTime;
        private int _targetNumber;
        private int _prevNumber;

        private void Awake() =>
            enabled = false;

        protected override void Run()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _duration)
            {
                SetText(_targetNumber);
                enabled = false;
            }

            SetText(GetAnimatedNumberByTime(_elapsedTime));
        }

        public void SetTextAnimated(int number)
        {
            _prevNumber = _targetNumber;
            _targetNumber = number;
            _elapsedTime = 0;
            enabled = true;
        }

        private int GetAnimatedNumberByTime(float time)
        {
            int difference = _targetNumber - _prevNumber;
            float animatedNumber = _curveAnimation.Evaluate(time / _duration);
            return _prevNumber + Mathf.RoundToInt(animatedNumber * difference);
        }

        [Conditional("UNITY_EDITOR")]
        [Button("Set Test Text")]
        private void SetTextNumber()
        {
            SetTextAnimated(_testNumber);
        }
    }
}