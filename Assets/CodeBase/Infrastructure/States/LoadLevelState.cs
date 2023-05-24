using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Player;
using CodeBase.Logic.Spawners;
using CodeBase.Services.Input;
using CodeBase.UI.Elements;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly Vector3 _heroDefaultPosition = new Vector3(42f, 0, 42f);
        private readonly Vector3 _moneySpawnerDefaultPosition = new Vector3(42f, 0, 63f);

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerInputService _playerInputService;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPlayerInputService playerInputService, LoadingCurtain curtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _playerInputService = playerInputService;
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
        }

        private void InitMoneySpawner()
        {
            GameObject moneySpawnerobject = _gameFactory.CreateMoneySpawner(_moneySpawnerDefaultPosition);
            var moneySpawner = moneySpawnerobject.GetComponent<MoneySpawner>();
            moneySpawner.Construct(_gameFactory);
            moneySpawner.Spawn();
        }

        private Vector3 GetHeroPosition() =>
            _heroDefaultPosition;

        private GameObject InitHero()
        {
            Vector3 heroPosition = GetHeroPosition();
            GameObject hero = _gameFactory.CreateHero(heroPosition);
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