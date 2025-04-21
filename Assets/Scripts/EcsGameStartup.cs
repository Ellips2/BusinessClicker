using GameWorld;
using Income;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;
using SaveLoad;
using UI;
using UI.Business;
using UI.LevelUpButton;
using UI.Markers;
using UI.Score;
using UI.UpgradeButton;
using UnityEngine;

internal class EcsStartup : MonoBehaviour
{
    [SerializeField] private UiRoot uiRoot;
    private IGameFactory _factory;
    private PlayerProgress _playerProgress;
    private ISaveLoadService _saveLoadService;

    private IStaticDataService _staticDataService;
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
        _uiEmitter = uiRoot.uiEmitter;
        _staticDataService = new StaticDataService();
        _staticDataService.Load();
        

        _saveLoadService = new SaveLoadService(_staticDataService);
        _playerProgress =
            _saveLoadService.Load()
            ?? _saveLoadService.NewProgress();

        _factory = new GameFactory(_world, uiRoot, _playerProgress, _staticDataService);
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