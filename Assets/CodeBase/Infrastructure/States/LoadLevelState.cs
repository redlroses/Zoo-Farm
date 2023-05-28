using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Builders;
using CodeBase.Logic.Interactions;
using CodeBase.Logic.Inventory;
using CodeBase.Logic.Player;
using CodeBase.Logic.Pool;
using CodeBase.Logic.Spawners;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerInputService _playerInputService;
        private readonly IStaticDataService _staticDataService;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPlayerInputService playerInputService, IStaticDataService staticDataService, LoadingCurtain curtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _playerInputService = playerInputService;
            _staticDataService = staticDataService;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            var hero = InitHero();
            InitHud(hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitMoneySpawner();
            InitRabbitFieldBuilder();
            InitCarrotField();
        }

        private void InitCarrotField()
        {
            GameObject carrotField =
                _gameFactory.CreateCarrotsField(_staticDataService.LocationFor(LocationKey.CarrotField));

            foreach (Builder builder in carrotField.GetComponentsInChildren<Builder>(true))
                builder.Construct(_gameFactory);
        }

        private void InitRabbitFieldBuilder()
        {
            GameObject rabbitField =
                _gameFactory.CreateRabbitFieldBuilder(_staticDataService.LocationFor(LocationKey.RabbitFieldBuilder));
            rabbitField.GetComponent<RabbitFieldBuilder>().Construct(_gameFactory);
        }

        private void InitMoneySpawner()
        {
            GameObject moneySpawnerobject =
                _gameFactory.CreateMoneySpawner(_staticDataService.LocationFor(LocationKey.MoneySpawner));
            var moneySpawner = moneySpawnerobject.GetComponent<MoneySpawner>();
            moneySpawner.Construct(_gameFactory);
            moneySpawner.Spawn();
        }

        private GameObject InitHero()
        {
            GameObject hero = _gameFactory.CreateHero(_staticDataService.LocationFor(LocationKey.Hero));
            hero.Enable();
            hero.GetComponent<Hero>().Construct(_playerInputService);
            hero.GetComponent<HeroMover>().Construct(_playerInputService);
            hero.GetComponent<HeroPools>().Construct(_gameFactory);
            HeroInventory heroInventory = hero.GetComponentInChildren<HeroInventory>();
            heroInventory.Construct();
            hero.GetComponentInChildren<InventoryView>().Construct(heroInventory.Inventory);
            FollowCamera(hero.transform);
            return hero;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            hud.Enable();
            hud.GetComponent<Canvas>().worldCamera = Camera.main;
            HeroInventory heroInventory = hero.GetComponentInChildren<HeroInventory>();
            hud.GetComponentInChildren<MoneyView>().Construct(heroInventory.Inventory);
        }

        private void FollowCamera(Transform to) =>
            Camera.main.GetComponentInParent<CameraFollower>().Follow(to);
    }
}