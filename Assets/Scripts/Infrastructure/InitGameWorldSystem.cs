using Infrastructure.Factory;
using Leopotam.EcsLite;
using Logic;
using SaveLoad;

namespace Infrastructure
{
    internal class InitGameWorldSystem : IEcsInitSystem
    {
        private readonly IGameFactory _factory;
        private readonly PlayerProgress _progress;
        private readonly IStaticDataService _staticDataService;
        private readonly UiRoot _uiRoot;

        public InitGameWorldSystem(IGameFactory factory)
        {
            _factory = factory;
        }

        public void Init(IEcsSystems systems)
        {
            _factory.CreateScore();
            _factory.CreateBusinessNodes();
        }
    }
}