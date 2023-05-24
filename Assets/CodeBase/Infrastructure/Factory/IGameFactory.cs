using CodeBase.Logic.Items;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void Cleanup();
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        GameObject CreateCollectible<TCollectible>(Vector3 at);
        GameObject CreateMoneySpawner(Vector3 moneySpawnerDefaultPosition);
    }
}