using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using UI.Markers;
using UI.Score;

namespace UI.Business
{
    public sealed class UpdateBusinessNodeViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly UiRoot _uiRoot;
        private readonly EcsWorld _world;
        
        private ScoreView _balanceView;
        private EcsPool<BusinessNode> _businessNodePool;
        private List<BusinessNodeView> _businessNodeViewList;
        private EcsFilter _filter;

        public UpdateBusinessNodeViewSystem(EcsWorld world, UiRoot uiRoot)
        {
            _world = world;
            _uiRoot = uiRoot;
        }

        public void Init(IEcsSystems systems)
        {
            _filter = _world.Filter<BusinessNode>().Inc<UpdateComponentViewEvent>().End();
            _businessNodePool = _world.GetPool<BusinessNode>();
            _businessNodeViewList = _uiRoot.GetComponentsInChildren<BusinessNodeView>().ToList();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _filter)
            {
                ref var businessNode = ref _businessNodePool.Get(index);
                foreach (var businessNodeView in _businessNodeViewList)
                    if (businessNodeView.Id == businessNode.Id)
                    {
                        businessNodeView.IncomeValue.text = businessNode.Income.ToString();
                        businessNodeView.LevelValue.text = businessNode.Level.ToString();
                        businessNodeView.LevelUpPriceValue.text = businessNode.LevelUpPrice.ToString();
                    }
            }
        }
    }
}