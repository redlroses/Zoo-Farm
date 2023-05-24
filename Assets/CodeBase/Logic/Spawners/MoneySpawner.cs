using System.Collections.Generic;
using System.Diagnostics;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Сollectible;
using NaughtyAttributes;
using UnityEngine;

namespace CodeBase.Logic.Spawners
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPlaces;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void Spawn()
        {
            foreach (Transform spawnPlace in _spawnPlaces)
            {
                GameObject moneyPack = _gameFactory.CreateMoneyPack(spawnPlace.position);
                moneyPack.transform.SetParent(transform);
            }
        }

        [Button("Collect All Spawn Places")] [Conditional("UNITY_EDITOR")]
        private void CollectAllSpawnPlaces()
        {
            _spawnPlaces.Clear();

            foreach (Transform childTransform in transform)
            {
                _spawnPlaces.Add(childTransform);
            }
        }
    }
}