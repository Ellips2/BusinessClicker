using Leopotam.EcsLite;
using GameWorld;
using SaveLoad;
using UI.Business;
using UI.Markers;
using Utils;

namespace UI.LevelUpButton
{
    internal class BuyLevelUpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameFactory _factory;
        private readonly IStaticDataService _staticDataService;
        private readonly EcsWorld _world;
        private EcsPool<BusinessNode> _businessNodePool;
        private EcsFilter _eventFilter;
        private EcsPool<UpdateComponentViewEvent> _updateComponentViewPool;

        public BuyLevelUpSystem(EcsWorld world, IGameFactory factory, IStaticDataService staticDataService)
        {
            _world = world;
            _factory = factory;
            _staticDataService = staticDataService;
        }

        public void Init(IEcsSystems systems)
        {
            _eventFilter = _world.Filter<BusinessNode>().Inc<BuyLevelUpEvent>().End();
            _businessNodePool = _world.GetPool<BusinessNode>();
            _updateComponentViewPool = _world.GetPool<UpdateComponentViewEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _eventFilter)
            {
                ref var businessNode = ref _businessNodePool.Get(index);

                var levelUpPrice = businessNode.LevelUpPrice;

                var score = _factory.Score;
                if (!score.HasEnoughScore(levelUpPrice)) return;

                score.SpendScore(levelUpPrice);

                LevelUpBusinessNode(ref businessNode);

                _updateComponentViewPool.SendUpdateComponentEvent(businessNode.EntityId);
            }
        }

        private void LevelUpBusinessNode(ref BusinessNode businessNode)
        {
            var businessStaticData = _staticDataService.ForBusiness(businessNode.Id);

            businessNode.Level += 1;
            businessNode.LevelUpPrice = CalculateBusinessNodeLevelUpPrice(ref businessNode, businessStaticData);
            businessNode.Income = businessNode.CalculateBusinessIncome(_staticDataService);
        }

        private int CalculateBusinessNodeLevelUpPrice(ref BusinessNode businessNode,
            BusinessStaticData businessStaticData)
        {
            return businessNode.Level * businessStaticData.DefaultPrice;
        }
    }
}