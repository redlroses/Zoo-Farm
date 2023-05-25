using CodeBase.MonoStateMachine;
using UnityEngine;

namespace CodeBase.Logic.Rabbit.States
{
    public class RabbitMoveState : State
    {
        [SerializeField] private RabbitAnimator _animator;
        [SerializeField] private RabbitMover _mover;

        private BoundsPosition _bounds;

        public void Construct(BoundsPosition bounds) =>
            _bounds = bounds;

        protected override void OnEnabled()
        {
            _animator.SetMove();
            Vector3 randomPositionInBounds = _bounds.GetRandomBoundsPosition();
            _mover.SetDestination(randomPositionInBounds);
            _mover.enabled = true;
        }

        protected override void OnDisabled() =>
            _mover.enabled = false;
    }
}