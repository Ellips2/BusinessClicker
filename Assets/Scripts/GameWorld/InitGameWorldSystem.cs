using Leopotam.EcsLite;
using SaveLoad;
using UI;

namespace GameWorld
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