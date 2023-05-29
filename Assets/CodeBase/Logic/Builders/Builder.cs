using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Interactions;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public abstract class Builder : MonoBehaviour
    {
        [SerializeField] private InteractionInventoryPaidZone _interactionZone;
        [SerializeField] private Transform _spawnPoint;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void OnEnable() =>
            _interactionZone.FullPaid += OnFillPaid;

        private void OnDisable() =>
            _interactionZone.FullPaid -= OnFillPaid;

        private void OnFillPaid()
        {
            Build(_gameFactory, _spawnPoint);
            gameObject.Disable();
        }

        protected abstract void Build(IGameFactory gameFactory, Transform at);
    }
}