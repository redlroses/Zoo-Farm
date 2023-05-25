using System;
using CodeBase.Logic.Rabbit.States;
using CodeBase.MonoStateMachine;
using UnityEngine;

namespace CodeBase.Logic.Rabbit.Transitions
{
    public class MoveToIdleTransition : Transition
    {
        [SerializeField] private RabbitMover _mover;
        [SerializeField] private float _destinationOffset = 0.1f;

        private Transform _selfTransform;

        private void Awake() =>
            _selfTransform = _mover.transform;

        protected override void Run()
        {
            float distance = Vector3.Distance(_selfTransform.position, _mover.DestinationPoint);

            if (distance <= _destinationOffset)
            {
                StateMachine.ChangeState<RabbitIdleState>();
            }
        }
    }
}