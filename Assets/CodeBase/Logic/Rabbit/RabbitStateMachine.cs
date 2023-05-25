using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Rabbit.States;
using CodeBase.MonoStateMachine;
using CodeBase.Tools;
using UnityEngine;

namespace CodeBase.Logic.Rabbit
{
    public class RabbitStateMachine : MonoStateMachine.MonoStateMachine
    {
        [SerializeField] [RequireInterface(typeof(ITransition))] private List<MonoBehaviour> _transitions;

        protected override void InitTransitions()
        {
            foreach (ITransition transition in _transitions.Cast<ITransition>())
            {
                transition.Init(this);
            }
        }

        protected override void SetDefaultState() =>
            ChangeState<RabbitIdleState>();

        protected override Dictionary<Type, IMonoState> GetStates() =>
            new Dictionary<Type, IMonoState>
            {
                [typeof(RabbitIdleState)] = GetComponentInChildren<RabbitIdleState>(),
                [typeof(RabbitMoveState)] = GetComponentInChildren<RabbitMoveState>(),
            };
    }
}