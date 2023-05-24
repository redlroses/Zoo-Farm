using System;
using CodeBase.Logic.Carrot;

namespace CodeBase.Logic.Plants
{
    public struct Plant
    {
        private readonly float _growthTime;

        private GrowthState _state;
        private float _lifeTime;

        public event Action<GrowthState> StateChanged;

        public GrowthState State
        {
            get => _state;
            set
            {
                _state = value;
                StateChanged?.Invoke(_state);
            }
        }

        public Plant(float growthTime) : this()
        {
            State = GrowthState.Sprout;
            _growthTime = growthTime;
        }

        public void Tick(float deltaTime)
        {
            if (_lifeTime >= _growthTime)
            {
                State = GrowthState.Ready;
                return;
            }

            _lifeTime += deltaTime;
        }
    }
}