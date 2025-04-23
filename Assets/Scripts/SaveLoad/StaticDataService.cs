using System.Collections.Generic;
using System.Linq;
using Logic.Business;
using Logic.Score;
using Logic.UpgradeButton;
using UnityEngine;

namespace SaveLoad
{
    internal class StaticDataService : IStaticDataService
    {
        private const string BusinessesPath = "StaticData/Businesses";
        private const string ScorePath = "StaticData/ScoreStaticData";
        private const string UpgradesPath = "StaticData/Upgrades";

        private Dictionary<BusinessTypeId, BusinessStaticData> _businesses;
        private ScoreStaticData _scoreStaticData;
        private Dictionary<UpgradeTypeId, UpgradeStaticData> _upgrades;


        public void LoadStaticData()
        {
            _businesses = Resources
                .LoadAll<BusinessStaticData>(BusinessesPath)
                .ToDictionary(x => x.Id, x => x);

            _upgrades = Resources
                .LoadAll<UpgradeStaticData>(UpgradesPath)
                .ToDictionary(x => x.UpgradeId, x => x);

            _scoreStaticData = Resources.Load<ScoreStaticData>(ScorePath);
        }

        public BusinessStaticData GetBusinessStaticData(BusinessTypeId businessNodeId)
        {
            return _businesses.TryGetValue(businessNodeId, out var staticData)
                ? staticData
                : null;
        }

        public ScoreStaticData GetScoreStaticData()
        {
            return _scoreStaticData;
        }

        public UpgradeStaticData GetUpgradeStaticData(UpgradeTypeId upgradeTypeId)
        {
            return _upgrades.TryGetValue(upgradeTypeId, out var staticData)
                ? staticData
                : null;
        }

        public List<UpgradeStaticData> GetUpgradeStaticDataList(BusinessTypeId businessTypeId)
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