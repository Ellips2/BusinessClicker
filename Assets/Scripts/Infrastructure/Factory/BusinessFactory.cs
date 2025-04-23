using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Leopotam.EcsLite;
using Logic;
using Logic.Business;
using Logic.Income;
using SaveLoad;

namespace Infrastructure.Factory
{
    internal class BusinessNodeFactory : IBusinessNodeFactory
    {
        private readonly EcsWorld _world;
        private readonly IStaticDataService _staticDataService;
        private readonly IUpgradeFactory _upgradeFactory;
        private readonly UiRoot _uiRoot;
        private readonly IAssetLoader _assetLoader;
        private readonly PlayerProgress _progress;

        public BusinessNodeFactory(EcsWorld world, IStaticDataService staticDataService,
            UiRoot uiRoot, IAssetLoader assetLoader, IUpgradeFactory upgradeFactory, PlayerProgress progress)
        {
            _world = world;
            _staticDataService = staticDataService;
            _uiRoot = uiRoot;
            _assetLoader = assetLoader;
            _upgradeFactory = upgradeFactory;
            _progress = progress;
        }

        public void CreateBusinessNodes()
        {
            foreach (var nodeData in _progress.BusinessNodes)
                CreateBusinessNode(nodeData);
        }

        private void CreateBusinessNode(BusinessNodeData data)
        {
            var staticData = _staticDataService.GetBusinessStaticData(data.Id);

            var entity = _world.NewEntity();
            ref var node = ref _world.GetPool<BusinessNode>().Add(entity);
            node = new BusinessNode
            {
                EntityId = entity,
                Id = data.Id,
                Level = data.Level,
                Income = data.Income,
                LevelUpPrice = data.LevelUpPrice,
                IncomeDelay = staticData.IncomeDelay,
                Upgrades = new List<EcsPackedEntityWithWorld>()
            };

            var nodeView = CreateBusinessNodeView();
            InitBusinessNodeView(nodeView, staticData, node);

            _upgradeFactory.CreateUpgrades(data.Upgrades, nodeView, ref node);

            _world.GetPool<IncomeTimer>().Add(entity).Timer = node.IncomeDelay;
        }

        private BusinessNodeView CreateBusinessNodeView()
            => _assetLoader.Instantiate(AssetPath.BusinessNode, _uiRoot.BusinessNodeContainer).GetComponent<BusinessNodeView>();

        private void InitBusinessNodeView(BusinessNodeView view, BusinessStaticData staticData, BusinessNode node)
        {
            view.Id = node.Id;
            view.Name.text = staticData.Name;
            view.LevelLabel.text = staticData.LevelLabel;
            view.IncomeLabel.text = staticData.IncomeLabel;
            view.LevelUpPriceLabel.text = staticData.LevelUpPriceLabel;

            view.LevelValue.text = node.Level.ToString();
            view.IncomeValue.text = node.Income.ToString();
            view.LevelUpPriceValue.text = node.LevelUpPrice.ToString();
        }
    }

}