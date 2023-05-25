using CodeBase.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void Cleanup();
        GameObject CreateHero(LocationStaticData at);
        GameObject CreateHud();
        // GameObject CreateCollectible<TCollectible>(Vector3 at);
        GameObject CreateMoneySpawner(LocationStaticData at);
        GameObject CreateRabbitField(Vector3 at, Quaternion rotation);
        GameObject CreateRabbitFieldBuilder(LocationStaticData at);
        GameObject CreateCarrotsField(LocationStaticData at);
        GameObject CreateCarrotPlantOperator(Vector3 atPosition);
        GameObject CreateSprout(Vector3 at, Quaternion rotation);
        GameObject CreateCarrotFruit(Vector3 at, Quaternion rotation);
        GameObject CreateMoneyPack(Vector3 at);
        GameObject CreateRabbit(Vector3 at);
    }
}