using System;
using UnityEngine;

namespace CodeBase.Logic.Rabbit
{
    [RequireComponent(typeof(TimerOperator))]
    public class Satiety : MonoBehaviour
    {
        [SerializeField] private int _maxLevel;
        [SerializeField] private int _currentLevel;

        [SerializeField] [Tooltip("Delay between ticks in seconds")]
        private float _fastingRate = 3f;

        [SerializeField] private int _hungerPerTick = 1;

        private TimerOperator _timer;

        public event Action Updated = () => { };

        public int CurrentLevel
        {
            get => _currentLevel;
            private set
            {
                _currentLevel = value;
                Updated.Invoke();
            }
        }

        public int MaxLevel => _maxLevel;

        private void Awake()
        {
            _timer ??= GetComponent<TimerOperator>();
            _timer.SetUp(_fastingRate, OnTick);
        }

        private void OnTick()
        {
            float newCurrentLevel = CurrentLevel - _hungerPerTick;

            if (newCurrentLevel < 0)
            {
                CurrentLevel = 0;
                return;
            }

            CurrentLevel -= _hungerPerTick;
            _timer.Restart();
        }

        public void Replenish(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Replenish amount cannot be negative");

            int newCurrentLevel = CurrentLevel + amount;

            if (newCurrentLevel >= _maxLevel)
            {
                CurrentLevel = _maxLevel;
                _timer.Restart();
                return;
            }

            CurrentLevel = newCurrentLevel;
        }
    }
}