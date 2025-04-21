using System.Collections.Generic;
using GameWorld;
using Leopotam.EcsLite;
using UI;
using UI.Business;
using UI.Markers;
using UI.UpgradeButton;

namespace SaveLoad
{
    public sealed class SaveGameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameFactory _factory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly EcsWorld _world;
        private EcsFilter _businessNodeFilter;
        private EcsPool<BusinessNode> _businessNodePool;
        private EcsFilter _upgradeFilter;
        private EcsPool<Upgrade> _upgradePool;
        private EcsFilter _saveEventFilter;

        public SaveGameSystem(EcsWorld world, ISaveLoadService saveLoadService, IGameFactory factory)
        {
            _world = world;
            _saveLoadService = saveLoadService;
            _factory = factory;
        }

        public void Init(IEcsSystems systems)
        {
            _saveEventFilter = _world.Filter<SaveEvent>().End();
            _businessNodeFilter = _world.Filter<BusinessNode>().End();
            _upgradeFilter = _world.Filter<Upgrade>().End();

            _businessNodePool = _world.GetPool<BusinessNode>();
            _upgradePool = _world.GetPool<Upgrade>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var saveEventIndex in _saveEventFilter)
            {
                var progress = new PlayerProgress();

                progress.BusinessNodes = GetBusinessNodeDataList();

                progress.ScoreData.Value = CalculateScore(progress);

                _saveLoadService.Save(progress);
            }
        }

        private float CalculateScore(PlayerProgress progress)
        {
            float allIncome = 0;
            foreach (var businessNodeData in progress.BusinessNodes)
                if (businessNodeData.Level > 0)
                    allIncome += businessNodeData.Income;
            return _factory.Score.Value - allIncome;
        }

        private List<BusinessNodeData> GetBusinessNodeDataList()
        {
            var businessNodeDataList = new List<BusinessNodeData>();
            foreach (var businessNodeIndex in _businessNodeFilter)
            {
                ref var businessNode = ref _businessNodePool.Get(businessNodeIndex);

                var businessNodeData = new BusinessNodeData();

                businessNodeData.Id = businessNode.Id;
                businessNodeData.Income = businessNode.Income;
                businessNodeData.Level = businessNode.Level;
                businessNodeData.LevelUpPrice = businessNode.LevelUpPrice;

                businessNodeData.Upgrades = GetUpgradeDataList(businessNode);

                businessNodeDataList.Add(businessNodeData);
            }

            return businessNodeDataList;
        }

        private List<UpgradeData> GetUpgradeDataList(BusinessNode businessNode)
        {
            var upgradeDataList = new List<UpgradeData>();
            foreach (var entityWithWorld in businessNode.Upgrades)
            {
                entityWithWorld.Unpack(out var world, out var packedUpgrade);
                ref var upgrade = ref _upgradePool.Get(packedUpgrade);

                var upgradeData = new UpgradeData();
                upgradeData.Id = upgrade.Id;
                upgradeData.Unlocked = upgrade.Unlocked;
                upgradeData.BusinessId = upgrade.BusinessId;

                upgradeDataList.Add(upgradeData);
            }

            //upgradeDataList.Reverse();

            return upgradeDataList;
        }
    }
}