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
        Item CreateItem<TItem>();
    }
}