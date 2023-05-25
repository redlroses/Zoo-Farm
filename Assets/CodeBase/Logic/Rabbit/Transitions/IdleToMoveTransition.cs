﻿using CodeBase.Logic.Rabbit.States;
using CodeBase.MonoStateMachine;
using NaughtyAttributes;
using UnityEngine;

namespace CodeBase.Logic.Rabbit.Transitions
{
    public sealed class IdleToMoveTransition : Transition
    {
        [SerializeField] [MinMaxSlider(0f, 10f)]
        private Vector2 _transitionDelay;

        private float _randomTransitionDelay;
        private float _elapsedTime;

        protected override void OnEnabled()
        {
            _randomTransitionDelay = Random.Range(_transitionDelay.x, _transitionDelay.y);
            _elapsedTime = 0;
        }

        protected override void Run()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _randomTransitionDelay)
            {
                StateMachine.ChangeState<RabbitMoveState>();
            }
        }
    }
}