using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        // private Dictionary<Type, Func<Vector3, GameObject>> _items;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
            // ConfigureCollectibles();
        }

        public void Cleanup() { }

        public GameObject CreateHero(LocationStaticData at)
        {
            GameObject hero = _assets.Instantiate(AssetPath.HeroPath, at.Position, at.Rotation);
            return hero;
        }

        // public GameObject CreateCollectible<TCollectible>(Vector3 at) =>
        //     _items[typeof(TCollectible)].Invoke(at);

        public GameObject CreateMoneySpawner(LocationStaticData at) =>
            _assets.Instantiate(AssetPath.MoneySpawner, at.Position, at.Rotation);

        public GameObject CreateRabbitField(Vector3 at, Quaternion rotation) =>
            _assets.Instantiate(AssetPath.RabbitFieldPath, at, rotation);

        public GameObject CreateRabbitFieldBuilder(LocationStaticData at) =>
            _assets.Instantiate(AssetPath.RabbitFieldBuilderPath, at.Position, at.Rotation);

        public GameObject CreateCarrotsField(LocationStaticData at) =>
            _assets.Instantiate(AssetPath.CarrotFieldPath, at.Position, at.Rotation);

        public GameObject CreateCarrotPlantOperator(Vector3 at) =>
            _assets.Instantiate(AssetPath.PlantOperatorPath, at);

        public GameObject CreateSprout(Vector3 at, Quaternion rotation) =>
            _assets.Instantiate(AssetPath.SproutPath, at, rotation);

        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HudPath);
            return hud;
        }

        public GameObject CreateCarrotFruit(Vector3 at, Quaternion rotation) =>
            _assets.Instantiate(AssetPath.CarrotFruitPath, at, rotation);

        public GameObject CreateMoneyPack(Vector3 at) =>
            _assets.Instantiate(AssetPath.MoneyPackPath, at);

        public GameObject CreateRabbit(Vector3 at) =>
            _assets.Instantiate(AssetPath.RabbitPath, at);

        // private void ConfigureCollectibles() =>
        //     _items = new Dictionary<Type, Func<Vector3, GameObject>>
        //     {
        //         [typeof(MoneyPack)] = CreateMoneyPack,
        //         [typeof(CarrotFruit)] = CreateCarrotFruit,
        //     };
    }
}
