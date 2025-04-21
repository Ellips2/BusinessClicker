using Leopotam.EcsLite;
using Services;
using UI.Markers;

namespace UI.Score
{
    public sealed class UpdateScoreViewSystem: IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameFactory _factory;
        private readonly UiRoot _uiRoot;
        private readonly EcsWorld _world;
        private ScoreView _scoreView;
        private EcsFilter _filter;

        public UpdateScoreViewSystem(EcsWorld world, IGameFactory factory, UiRoot uiRoot)
        {
            _world = world;
            _factory = factory;
            _uiRoot = uiRoot;
        }

        public void Init(IEcsSystems systems)
        {
            _filter = _world.Filter<UpdateComponentViewEvent>().End();
            _scoreView = _uiRoot.scorePanel.GetComponentInChildren<ScoreView>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _filter) _scoreView.Value.text = _factory.Score.Value.ToString();
        }
    }
}