using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.Logic.Items;
using CodeBase.Logic.Movement;
using CodeBase.Logic.Player;
using CodeBase.Services.Input;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string CarrotName = "Carrot";

        private readonly IAssetProvider _assets;
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerInputService _inputService;

        private Dictionary<Type, Func<Item>> _items;

        public GameFactory(IAssetProvider assets, IPlayerInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
            ConfigureItems();
        }

        public void Cleanup() { }

        public GameObject CreateHero(Vector3 at)
        {
            GameObject hero = _assets.Instantiate(AssetPath.HeroPath, at);
            hero.Enable();
            hero.GetComponent<Hero>().Construct(_inputService);
            hero.GetComponent<HeroMover>().Construct(_inputService);
            FollowCamera(hero.transform);
            return hero;
        }

        public Item CreateItem<TItem>() =>
            _items[typeof(TItem)].Invoke();

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            return hud;
        }

        private void FollowCamera(Transform to) =>
            Camera.main.GetComponentInParent<CameraFollower>().Follow(to);

        private void ConfigureItems() =>
            _items = new Dictionary<Type, Func<Item>>
            {
                [typeof(Carrot)] = () => new Carrot(CarrotName),
            };
    }
}