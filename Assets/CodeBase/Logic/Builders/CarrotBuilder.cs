using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Plants;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public class CarrotBuilder : Builder
    {
        protected override void Build(IGameFactory gameFactory, Transform at)
        {
            Vector3 atPosition = at.position;
            Quaternion atRotation = at.rotation;

            GameObject carrotPlant = gameFactory.CreateCarrotPlantOperator(atPosition);
            GameObject sprout = gameFactory.CreateSprout(atPosition, atRotation);
            GameObject fruit = gameFactory.CreateCarrotFruit(atPosition, atRotation);

            PlantOperator plantOperator = carrotPlant.GetComponent<PlantOperator>();

            Transform operatorTransform = plantOperator.transform;
            sprout.transform.parent = operatorTransform;
            fruit.transform.parent = operatorTransform;

            fruit.Disable();
            sprout.Enable();

            plantOperator.Construct(sprout, fruit);
        }
    }
}