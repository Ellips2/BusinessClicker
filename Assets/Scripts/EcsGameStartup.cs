using Leopotam.EcsLite;
using UnityEngine;

internal class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private IEcsSystems _systems;

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
        
    }

    private void InitSystems()
    {
        
    }
}