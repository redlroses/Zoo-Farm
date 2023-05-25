using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Rabbit.States;
using CodeBase.Tools.Extension;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public class RabbitFieldBuilder : Builder
    {
        private const float InitialHeight = 0.25f;

        [SerializeField] private int _startRabbitsCount;

        protected override void Build(IGameFactory gameFactory, Transform at)
        {
            GameObject field = gameFactory.CreateRabbitField(at.position, at.rotation);
            BoundsPosition bounds = field.GetComponentInChildren<BoundsPosition>();

            for (int i = 0; i < _startRabbitsCount; i++)
            {
                Vector3 randomSpawnPoint = bounds.GetRandomBoundsPosition();
                GameObject rabbit = gameFactory.CreateRabbit(randomSpawnPoint.ChangeY(InitialHeight));
                rabbit.GetComponentInChildren<RabbitMoveState>().Construct(bounds);
                rabbit.Enable();
            }
        }
    }
}