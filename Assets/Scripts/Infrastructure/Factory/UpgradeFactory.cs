using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Leopotam.EcsLite;
using Logic.Business;
using Logic.Markers;
using Logic.UpgradeButton;
using SaveLoad;

namespace Infrastructure.Factory
{
    internal class UpgradeFactory : IUpgradeFactory
    {
        private readonly EcsWorld _world;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetLoader _assetLoader;

        public UpgradeFactory(EcsWorld world, IStaticDataService staticDataService, IAssetLoader assetLoader)
        {
            _world = world;
            _staticDataService = staticDataService;
            _assetLoader = assetLoader;
        }

        public void CreateUpgrades(List<UpgradeData> upgrades, BusinessNodeView view, ref BusinessNode node)
        {
            foreach (var upgradeData in upgrades)
                CreateUpgrade(upgradeData, view, ref node);
        }

        private void CreateUpgrade(UpgradeData data, BusinessNodeView view, ref BusinessNode node)
        {
            var staticData = _staticDataService.GetUpgradeStaticData(data.UpgradeId);

            var entity = _world.NewEntity();
            ref var upgrade = ref _world.GetPool<Upgrade>().Add(entity);
            upgrade = new Upgrade
            {
                TypeId = data.UpgradeId,
                BusinessId = data.BusinessId,
                Unlocked = data.Unlocked,
                Price = staticData.Price,
                IncomeMultiplierPercent = staticData.IncomeMultiplierPercent
            };

            var upgradeView = CreateUpgradeView(view);
            InitUpgradeView(data, upgradeView, staticData);

            node.Upgrades.Add(_world.PackEntityWithWorld(entity));

            if (upgrade.Unlocked)
                _world.GetPool<Unlocked>().Add(entity);
        }

        private UpgradeView CreateUpgradeView(BusinessNodeView parentView)
            => _assetLoader.Instantiate(AssetPath.Upgrade, parentView.UpgradeRoot).GetComponent<UpgradeView>();

        private void InitUpgradeView(UpgradeData data, UpgradeView view, UpgradeStaticData staticData)
        {
            view.UpgradeId = data.UpgradeId;
            view.Name.text = staticData.Name;
            view.IncomeLabel.text = staticData.IncomeLabel;
            view.PriceLabel.text = staticData.PriceLabel;
            view.IncomeValue.text = staticData.IncomeMultiplierPercent.ToString();
            view.PriceValue.text = staticData.Price.ToString();
        }
    }
}