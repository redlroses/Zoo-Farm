using CodeBase.MonoStateMachine;
using UnityEngine;

namespace CodeBase.Logic.Rabbit.States
{
    public class RabbitMoveState : State
    {
        [SerializeField] private RabbitAnimator _animator;
        [SerializeField] private RabbitMover _mover;
        [SerializeField] private BoxCollider _bounds;

        protected override void OnEnabled()
        {
            _animator.SetMove();
            Vector3 randomPositionInBounds = GetRandomPosition();
            _mover.SetDestination(randomPositionInBounds);
            _mover.enabled = true;
            Debug.Log("Enter MoveState");
        }

        protected override void OnDisabled() =>
            _mover.enabled = false;

        private Vector3 GetRandomPosition()
        {
            Bounds randomField = _bounds.bounds;
            float randomX = Random.Range(randomField.min.x, randomField.max.x);
            float randomZ = Random.Range(randomField.min.z, randomField.max.z);
            return new Vector3(randomX, 0, randomZ);
        }
    }
}