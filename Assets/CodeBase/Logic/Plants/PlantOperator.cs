using System;
using CodeBase.Logic.Carrot;
using CodeBase.Logic.Сollectible;
using NTC.Global.Cache;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Plants
{
    public class PlantOperator : MonoCache
    {
        [SerializeField] private float _growthTime;

        private GameObject _sprout;
        private GameObject _fruit;
        private GameObject _currentMeshObject;

        private Plant _plant;

        public void Construct(GameObject sprout, GameObject fruit)
        {
            _sprout = sprout;
            _fruit = fruit;
            _currentMeshObject = _sprout;
            Subscribe();
            PlantNewPlant();
        }

        protected override void Run() =>
            _plant.Tick(Time.deltaTime);

        private void Subscribe() =>
            _fruit.GetComponent<ICollectibleItemReactive>().Collected += OnCollected;

        private void OnCollected() =>
            PlantNewPlant();

        private void PlantNewPlant()
        {
            _plant = new Plant(_growthTime);
            _plant.StateChanged += OnStateChanged;
            ChangeMeshObject(_sprout);
            enabled = true;
        }

        private void ChangeMeshObject(GameObject to)
        {
            _currentMeshObject.Disable();
            _currentMeshObject = to;
            _currentMeshObject.Enable();
        }

        private void OnStateChanged(GrowthState state)
        {
            switch (state)
            {
                case GrowthState.Sprout:
                    break;
                case GrowthState.Ready:
                    FinishGrowth();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void FinishGrowth()
        {
            enabled = false;
            ChangeMeshObject(_fruit);
            _plant.StateChanged -= OnStateChanged;
        }
    }
}