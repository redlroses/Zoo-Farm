using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Plants;
using CodeBase.Logic.Сollectible;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public class CarrotBuilder : Builder
    {
        protected override void Build(IGameFactory gameFactory, Transform at)
        {
            Vector3 atPosition = at.position;
            GameObject carrotPlant = gameFactory.CreateCarrotPlant(atPosition);
            PlantOperator plantOperator = carrotPlant.GetComponent<PlantOperator>();
            GameObject sprout = gameFactory.CreateSprout(atPosition);
            GameObject fruit = gameFactory.CreateCarrotFruit(atPosition);
            sprout.transform.parent = plantOperator.transform;
            fruit.GetComponent<CarrotFruit>().Construct(new Items.Carrot());
            fruit.transform.parent = plantOperator.transform;
            fruit.Disable();
            sprout.Enable();
            plantOperator.Construct(sprout, fruit);
        }
    }
}