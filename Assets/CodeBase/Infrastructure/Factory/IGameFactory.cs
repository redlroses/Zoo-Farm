using CodeBase.Logic.Items;
using CodeBase.Services;
using CodeBase.StaticData.Storable;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void Cleanup();
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        IItem CreateItem(StorableType itemType);
    }
}