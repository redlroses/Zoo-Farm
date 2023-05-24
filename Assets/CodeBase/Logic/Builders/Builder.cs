using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Interactions;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public abstract class Builder : MonoBehaviour
    {
        [SerializeField] private InteractionPaidZone _interactionzone;
        [SerializeField] private Transform _spawnPoint;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void OnEnable() =>
            _interactionzone.FullPaid += OnFillPaid;

        private void OnDisable() =>
            _interactionzone.FullPaid -= OnFillPaid;

        private void OnFillPaid() =>
            Build(_gameFactory, _spawnPoint);

        protected abstract void Build(IGameFactory gameFactory, Transform at);
    }
}