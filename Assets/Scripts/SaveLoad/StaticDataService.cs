using System.Collections.Generic;
using System.Linq;
using UI.Business;
using UI.Score;
using UI.UpgradeButton;
using UnityEngine;

namespace SaveLoad
{
    internal class StaticDataService : IStaticDataService
    {
        private const string BusinessesPath = "Static Data/Businesses";
        private const string ScorePath = "Static Data/Score/ScoreStaticData";
        private const string UpgradesPath = "Static Data/Upgrades";

        private Dictionary<BusinessTypeId, BusinessStaticData> _businesses;
        private ScoreStaticData _scoreStaticData;
        private Dictionary<UpgradeId, UpgradeStaticData> _upgrades;


        public void Load()
        {
            _businesses = Resources
                .LoadAll<BusinessStaticData>(BusinessesPath)
                .ToDictionary(x => x.Id, x => x);

            _upgrades = Resources
                .LoadAll<UpgradeStaticData>(UpgradesPath)
                .ToDictionary(x => x.Id, x => x);

            _scoreStaticData = Resources.Load<ScoreStaticData>(ScorePath);
        }

        public BusinessStaticData ForBusiness(BusinessTypeId businessNodeId)
        {
            return _businesses.TryGetValue(businessNodeId, out var staticData)
                ? staticData
                : null;
        }

        public ScoreStaticData ForScore()
        {
            return _scoreStaticData;
        }

        public UpgradeStaticData ForUpgrade(UpgradeId upgradeId)
        {
            return _upgrades.TryGetValue(upgradeId, out var staticData)
                ? staticData
                : null;
        }

        public List<UpgradeStaticData> ForUpgrades(BusinessTypeId businessTypeId)
        {
            var upgradeStaticDataList = new List<UpgradeStaticData>();
            foreach (var upgradeStaticData in _upgrades.Values)
                if (upgradeStaticData.BusinessId == businessTypeId)
                    upgradeStaticDataList.Add(upgradeStaticData);

            upgradeStaticDataList.Reverse();
            return upgradeStaticDataList;
        }
    }
}