using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public class CarrotBuilder : Builder
    {
        protected override void Build(IGameFactory gameFactory, Transform at)
        {
            gameFactory.CreateCarrot(at.position);
        }
    }
}