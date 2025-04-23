using Infrastructure.Factory;
using Leopotam.EcsLite;
using Logic.Business;
using Logic.Markers;
using Logic.UpgradeButton;
using SaveLoad;
using UnityEngine;
using Utils;

namespace Logic.Income
{
    public sealed class IncomeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameFactory _factory;
        private readonly IStaticDataService _staticDataService;
        private readonly EcsWorld _world;
        
        private EcsPool<BusinessNode> _businessNodesPool;
        private EcsFilter _filter;
        private EcsPool<IncomeTimer> _incomeTimers;
        private EcsPool<Upgrade> _upgradePool;
        private EcsPool<SaveEvent> _saveEventPool;
        private EcsPool<UpdateScoreViewEvent> _updateScorePool;
        private EcsPool<UpdateComponentViewEvent> _updateComponentViewPool;

        public IncomeSystem(EcsWorld world, IGameFactory factory, IStaticDataService staticDataService)
        {
            _world = world;
            _factory = factory;
            _staticDataService = staticDataService;
        }

        public void Init(IEcsSystems systems)
        {
            _filter = _world.Filter<BusinessNode>().End();

            _incomeTimers = _world.GetPool<IncomeTimer>();

            _businessNodesPool = _world.GetPool<BusinessNode>();
            _updateComponentViewPool = _world.GetPool<UpdateComponentViewEvent>();
            _updateScorePool = _world.GetPool<UpdateScoreViewEvent>();
            _upgradePool = _world.GetPool<Upgrade>();

            _saveEventPool = _world.GetPool<SaveEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _filter)
            {
                ref var incomeTimer = ref _incomeTimers.Get(index);
                ref var businessNode = ref _businessNodesPool.Get(index);

                if (businessNode.Level <= 0) return;

                incomeTimer.Timer -= Time.deltaTime;

                if (incomeTimer.Timer <= 0)
                {
                    businessNode.Income = businessNode.CalculateBusinessIncome(_staticDataService);

                    _factory.Score.Value += businessNode.Income;

                    _updateScorePool.Add(_world.NewEntity());

                    _updateComponentViewPool.SendUpdateComponentEvent(businessNode.EntityId);

                    _saveEventPool.SendSaveEvent(businessNode.EntityId);

                    ResetTimer(ref businessNode, ref incomeTimer);
                }
            }
        }

        private void ResetTimer(ref BusinessNode businessNode, ref IncomeTimer incomeTimer)
        {
            incomeTimer.Timer = businessNode.IncomeDelay;
        }
    }
}