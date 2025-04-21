using System.Collections.Generic;
using Leopotam.EcsLite;
using UI;
using UI.BusinessNode;
using UI.Markers;
using UI.Score;
using UI.UpgradeButton;
using UnityEngine;

namespace Services
{
    internal class GameFactory : IGameFactory
    {
        private readonly PlayerProgress _progress;
        private readonly IStaticDataService _staticDataService;
        private readonly UiRoot _uiRoot;
        private readonly EcsWorld _world;

        public GameFactory(EcsWorld world, UiRoot uiRootRoot, PlayerProgress progress,
            IStaticDataService staticDataService)
        {
            _world = world;
            _uiRoot = uiRootRoot;
            _progress = progress;
            _staticDataService = staticDataService;
        }

        public Score Score { get; set; }

        public void CreateScore()
        {
            Score = new Score
            {
                Value = _progress.ScoreData.Value
            };

            var scoreView = _uiRoot.scorePanel.GetComponentInChildren<ScoreView>();
            scoreView.ScoreLabel.text = _staticDataService.ForScore().ScoreLabel;
            scoreView.Value.text = Score.Value.ToString();
        }

        public void CreateBusinessNodes()
        {
            var businessNodes = _progress.BusinessNodes;
            foreach (var businessNodeFromSave in businessNodes) CreateBusinessNode(businessNodeFromSave);
        }

        private void CreateBusinessNode(BusinessNodeData businessNodeFromSave)
        {
            var entity = _world.NewEntity();
            ref var businessNode = ref _world.GetPool<UI.BusinessNode.BusinessNode>().Add(entity);

            var businessStaticData = _staticDataService.ForBusiness(businessNodeFromSave.Id);

            businessNode.EntityId = entity;
            businessNode.Id = businessNodeFromSave.Id;
            businessNode.Level = businessNodeFromSave.Level;
            businessNode.Income = businessNodeFromSave.Income;
            businessNode.LevelUpPrice = businessNodeFromSave.LevelUpPrice;
            businessNode.Upgrades = new List<EcsPackedEntityWithWorld>();

            businessNode.IncomeDelay = businessStaticData.IncomeDelay;

            var businessNodeObj = new GameObject("node");

            var businessNodeView = businessNodeObj.GetComponent<BusinessNodeView>();

            businessNodeView.Name.text = businessStaticData.Name;
            businessNodeView.LevelUpPriceLabel.text = businessStaticData.LevelUpPriceLabel;
            businessNodeView.IncomeLabel.text = businessStaticData.IncomeLabel;
            businessNodeView.LevelLabel.text = businessStaticData.LevelLabel;

            businessNodeView.Id = businessNode.Id;
            businessNodeView.LevelValue.text = businessNode.Level.ToString();
            businessNodeView.IncomeValue.text = businessNode.Income.ToString();
            businessNodeView.LevelUpPriceValue.text = businessNode.LevelUpPrice.ToString();

            CreateUpgrades(businessNodeFromSave, businessNodeView, ref businessNode);

            var incomeTimer = _world.GetPool<IncomeTimer>().Add(entity);
            incomeTimer.Timer = businessNode.IncomeDelay;
        }

        private void CreateUpgrades(BusinessNodeData businessNodeFromSave, BusinessNodeView businessNodeView,
            ref UI.BusinessNode.BusinessNode businessNode)
        {
            var upgrades = businessNodeFromSave.Upgrades;
            
            foreach (var upgradeFromSave in upgrades)
                CreateUpgrade(businessNodeView, upgradeFromSave, ref businessNode);
        }

        private void CreateUpgrade(BusinessNodeView businessNodeView, UpgradeData upgradeFromSave,
            ref UI.BusinessNode.BusinessNode businessNode)
        {
            var entity = _world.NewEntity();
            ref var upgrade = ref _world.GetPool<Upgrade>().Add(entity);

            var upgradeStaticData = _staticDataService.ForUpgrade(upgradeFromSave.Id);

            upgrade.Id = upgradeFromSave.Id;
            upgrade.BusinessId = upgradeFromSave.BusinessId;
            upgrade.Unlocked = upgradeFromSave.Unlocked;

            upgrade.Price = upgradeStaticData.Price;
            upgrade.IncomeMultiplierPercent = upgradeStaticData.IncomeMultiplierPercent;

            var upgradeObj = new GameObject("upgrade");

            var upgradeView = upgradeObj.GetComponent<UpgradeView>();

            upgradeView.Id = upgradeFromSave.Id;

            upgradeView.Name.text = upgradeStaticData.Name;
            upgradeView.IncomeLabel.text = upgradeStaticData.IncomeLabel;
            upgradeView.PriceLabel.text = upgradeStaticData.PriceLabel;

            upgradeView.IncomeValue.text = upgradeStaticData.IncomeMultiplierPercent.ToString();
            upgradeView.PriceValue.text = upgradeStaticData.Price.ToString();

            var packed = _world.PackEntityWithWorld(entity);

            businessNode.Upgrades.Add(packed);

            if (upgrade.Unlocked)
                _world.GetPool<Unlocked>().Add(entity);
        }
    }
}