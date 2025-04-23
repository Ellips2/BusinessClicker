using Infrastructure.Factory;
using Leopotam.EcsLite;
using Logic.Business;
using Logic.Markers;
using SaveLoad;
using Utils;

namespace Logic.UpgradeButton
{
    internal sealed class BuyUpgradeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameFactory _factory;
        private readonly IStaticDataService _staticDataService;
        private readonly EcsWorld _world;
        
        private EcsFilter _businessFilter;
        private EcsPool<BusinessNode> _businessPool;
        private EcsFilter _eventFilter;
        private EcsPool<BuyUpgradeEvent> _eventPool;
        private EcsPool<Upgrade> _upgradePool;
        private EcsPool<Unlocked> _unlockedPool;
        private EcsPool<UpdateComponentViewEvent> _updateComponentViewPool;

        public BuyUpgradeSystem(EcsWorld world, IGameFactory factory, IStaticDataService staticDataService)
        {
            _world = world;
            _factory = factory;
            _staticDataService = staticDataService;
        }

        public void Init(IEcsSystems systems)
        {
            _eventFilter = _world.Filter<Upgrade>().Inc<BuyUpgradeEvent>().End();
            _businessFilter = _world.Filter<BusinessNode>().End();
            _upgradePool = _world.GetPool<Upgrade>();
            _businessPool = _world.GetPool<BusinessNode>();
            _updateComponentViewPool = _world.GetPool<UpdateComponentViewEvent>();
            _unlockedPool = _world.GetPool<Unlocked>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _eventFilter)
            {
                ref var upgrade = ref _upgradePool.Get(index);

                var score = _factory.Score;
                if (!score.HasEnoughScore(upgrade.Price)) return;

                score.SpendScore(upgrade.Price);

                upgrade.Unlocked = true;
                _unlockedPool.Add(index);

                UpgradeBusinessIncome(ref upgrade);
            }
        }

        private void UpgradeBusinessIncome(ref Upgrade upgrade)
        {
            foreach (var index in _businessFilter)
            {
                ref var businessCard = ref _businessPool.Get(index);
                if (upgrade.BusinessId == businessCard.Id)
                {
                    businessCard.Income = businessCard.CalculateBusinessIncome(_staticDataService);

                    _updateComponentViewPool.SendUpdateComponentEvent(businessCard.EntityId);
                }
            }
        }
    }
}