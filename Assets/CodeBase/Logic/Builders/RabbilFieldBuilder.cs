using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public class RabbilFieldBuilder : Builder
    {
        protected override void Build(IGameFactory gameFactory, Transform at)
        {
            GameObject field = gameFactory.CreateRabbitField(at.position, at.rotation);
            field.transform.rotation = at.rotation;
        }
    }
}