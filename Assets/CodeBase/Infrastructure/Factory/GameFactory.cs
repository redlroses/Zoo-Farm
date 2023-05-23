using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Items;
using CodeBase.Logic.Movement;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using CodeBase.StaticData.Storable;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerInputService _inputService;

        public GameFactory(IAssetProvider assets, IPlayerInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
        }

        public void Cleanup() { }

        public GameObject CreateHero(Vector3 at)
        {
            GameObject hero = _assets.Instantiate(AssetPath.HeroPath, at);
            hero.GetComponent<Hero>().Construct(_inputService);
            hero.GetComponent<HeroMover>().Construct(_inputService);
            FollowCamera(hero.transform);
            return hero;
        }

        public IItem CreateItem(StorableType data) =>
            data switch
            {
                StorableType.None => throw new Exception("Storable type is not specified"),
                _ => new Item(data)
            };

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            return hud;
        }

        private void FollowCamera(Transform to) =>
            Camera.main.GetComponentInParent<CameraFollower>().Follow(to);
    }
}