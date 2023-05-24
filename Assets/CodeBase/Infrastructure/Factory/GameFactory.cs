using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.Items;
using CodeBase.Logic.Movement;
using CodeBase.Logic.Player;
using CodeBase.Logic.Сollectible;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerInputService _inputService;
        private readonly IStaticDataService _staticData;

        private Dictionary<Type, Func<Vector3, GameObject>> _items;

        public GameFactory(IAssetProvider assets, IPlayerInputService inputService, IStaticDataService staticData)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            ConfigureItems();
        }

        public void Cleanup() { }

        public GameObject CreateHero(LocationStaticData at)
        {
            GameObject hero = _assets.Instantiate(AssetPath.HeroPath, at.Position, at.Rotation);
            hero.Enable();
            hero.GetComponent<Hero>().Construct(_inputService);
            hero.GetComponent<HeroMover>().Construct(_inputService);
            FollowCamera(hero.transform);
            return hero;
        }

        public GameObject CreateCollectible<TCollectible>(Vector3 at) =>
            _items[typeof(TCollectible)].Invoke(at);

        public GameObject CreateMoneySpawner(LocationStaticData at) =>
            _assets.Instantiate(AssetPath.MoneySpawner, at.Position, at.Rotation);

        public GameObject CreateRabbitField(Vector3 at, Quaternion rotation) =>
            _assets.Instantiate(AssetPath.RabbitFieldPath, at, rotation);

        public GameObject CreateRabbitFieldBuilder(LocationStaticData at) =>
            _assets.Instantiate(AssetPath.RabbitFieldBuilderPath, at.Position, at.Rotation);

        public GameObject CreateCarrotSField(LocationStaticData at) =>
            _assets.Instantiate(AssetPath.CarrotFieldPath, at.Position, at.Rotation);

        public void CreateCarrot(Vector3 at) =>
            _assets.Instantiate(AssetPath.CarrotPath, at);

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            return hud;
        }

        private void FollowCamera(Transform to) =>
            Camera.main.GetComponentInParent<CameraFollower>().Follow(to);

        private void ConfigureItems() =>
            _items = new Dictionary<Type, Func<Vector3, GameObject>>
            {
                [typeof(MoneyPack)] = at => CreateMoneyPack(at),
            };

        private GameObject CreateMoneyPack(Vector3 at)
        {
            GameObject moneyPackGameObject = _assets.Instantiate(AssetPath.MoneyPackPath, at);
            MoneyPack moneyPack = moneyPackGameObject.GetComponent<MoneyPack>();
            moneyPack.Construct(new Money(_staticData.MoneyPackConfig.AmountMoneyInPack));
            return moneyPackGameObject;
        }
    }
}
