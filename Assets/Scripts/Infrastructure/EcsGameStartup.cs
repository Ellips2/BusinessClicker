using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;
using Logic;
using Logic.Business;
using Logic.Income;
using Logic.LevelUpButton;
using Logic.Markers;
using Logic.Score;
using Logic.UpgradeButton;
using SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    internal class EcsStartup : MonoBehaviour
    {
        [SerializeField] private UiRoot uiRoot;
    
        private IGameFactory _factory;
        private IScoreFactory _scoreFactory;
        private IBusinessNodeFactory _businessNodeFactory;
        private IUpgradeFactory _upgradeFactory;
    
        private PlayerProgress _playerProgress;
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;
        private IAssetLoader _assetLoader;
        private IEcsSystems _systems;
        private EcsUguiEmitter _uiEmitter;
        private EcsWorld _world;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            InitServices();
            InitSystems();
        }

        private void Update() => _systems?.Run();

        private void OnDestroy() => ClearEcsData();

        private void ClearEcsData()
        {
            _systems?.Destroy();
            _world?.Destroy();

            _systems = null;
            _world = null;
        }

        private void InitServices()
        {
            _uiEmitter = uiRoot.UiEmitter;
        
            _staticDataService = new StaticDataService();
            _staticDataService.LoadStaticData();

            _saveLoadService = new SaveLoadService(_staticDataService);
            _playerProgress = _saveLoadService.LoadProgress() ?? _saveLoadService.NewProgress();
        
            _assetLoader = new AssetLoader();

            _scoreFactory = new ScoreFactory(_playerProgress, _staticDataService, uiRoot);
            _upgradeFactory = new UpgradeFactory(_world, _staticDataService, _assetLoader);
            _businessNodeFactory = new BusinessNodeFactory(_world, _staticDataService, uiRoot, _assetLoader, _upgradeFactory, _playerProgress);
            _factory = new GameFactory(_scoreFactory, _businessNodeFactory);
        }

        private void InitSystems()
        {
            _systems
                .Add(new UiClickEventSystem(_world))
                .Add(new InitGameWorldSystem(_factory))
                .Add(new IncomeSystem(_world, _factory, _staticDataService))
                .Add(new BuyUpgradeSystem(_world, _factory, _staticDataService))
                .Add(new BuyLevelUpSystem(_world, _factory, _staticDataService))
                .Add(new UpdateScoreViewSystem(_world, _factory, uiRoot))
                .Add(new UpdateBusinessNodeViewSystem(_world, uiRoot))
                .Add(new UpdateUpgradeViewSystem(_world, _factory, uiRoot))
                .Add(new UpdateSliderSystem(_world, uiRoot))
                .Add(new SaveGameSystem(_world, _saveLoadService, _factory))
                .DelHere<Clicked>()
                .DelHere<UpdateComponentViewEvent>()
                .DelHere<UpdateScoreViewEvent>()
                .DelHere<BuyUpgradeEvent>()
                .DelHere<BuyLevelUpEvent>()
                .DelHere<SaveEvent>()
#if UNITY_EDITOR
            //.Add(new EcsWorldDebugSystem())
#endif
                .InjectUgui(_uiEmitter)
                .Init();
        }
    }
}