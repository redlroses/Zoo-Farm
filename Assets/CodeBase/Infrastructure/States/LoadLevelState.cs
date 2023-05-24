using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Builders;
using CodeBase.Logic.Player;
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
        private readonly Vector3 _heroDefaultPosition = new Vector3(42f, 0, 42f);
        private readonly Vector3 _moneySpawnerDefaultPosition = new Vector3(42f, 0, 63f);
        private readonly Vector3 _rabbitFieldBuilderDefaultPosition = new Vector3(48f, 0, 50f);
        private readonly Vector3 _carrotFieldBuilderDefaultPosition = new Vector3(37f, 0, 52f);

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
            GameObject carrotField = _gameFactory.CreateCarrotField(_staticDataService.LocationFor(LocationKey.CarrotField));
        }

        private void InitRabbitFieldBuilder()
        {
            GameObject rabbitField =
                _gameFactory.CreateRabbitFieldBuilder(_staticDataService.LocationFor(LocationKey.RabbitFieldBuilder));
            rabbitField.GetComponent<RabbilFieldBuilder>().Construct(_gameFactory);
        }

        private void InitMoneySpawner()
        {
            GameObject moneySpawnerobject =
                _gameFactory.CreateMoneySpawner(_staticDataService.LocationFor(LocationKey.MoneySpawner));
            var moneySpawner = moneySpawnerobject.GetComponent<MoneySpawner>();
            moneySpawner.Construct(_gameFactory);
            moneySpawner.Spawn();
        }

        private Vector3 GetHeroPosition() =>
            _heroDefaultPosition;

        private GameObject InitHero()
        {
            Vector3 heroPosition = GetHeroPosition();
            GameObject hero = _gameFactory.CreateHero(_staticDataService.LocationFor(LocationKey.Hero));
            return hero;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            hud.Enable();
            hud.GetComponent<Canvas>().worldCamera = Camera.main;
            hud.GetComponentInChildren<WalletView>().Construct(hero.GetComponentInChildren<HeroWallet>().Wallet);
        }
    }
}