using CodeBase.MonoStateMachine;
using UnityEngine;

namespace CodeBase.Logic.Rabbit.States
{
    public class RabbitIdleState : State
    {
        [SerializeField] private RabbitAnimator _animator;

        protected override void OnEnabled()
        {
            _animator.SetIdle();
            Debug.Log("Enter IdleState");
        }
    }
}