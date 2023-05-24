using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(LevelNames.Initial, onLoaded: EnterLoadLevel);

        public void Exit() { }

        private void RegisterServices()
        {
            RegisterStaticDataService();
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPlayerInputService>(new PlayerInputService());
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(
                    _services.Single<IAssetProvider>(),
                    _services.Single<IStaticDataService>()));
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>(LevelNames.Main);

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }
    }
}