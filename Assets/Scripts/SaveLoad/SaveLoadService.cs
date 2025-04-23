using System.Collections.Generic;
using UI;
using UI.Business;
using UI.UpgradeButton;
using UnityEngine;

namespace SaveLoad
{
    internal class SaveLoadService : ISaveLoadService
    {
        private readonly IStaticDataService _staticDataService;

        public string ProgressKey = "PlayerProgress";

        public SaveLoadService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Save(PlayerProgress progress)
        {
            PlayerPrefs.SetString(ProgressKey, progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
        }

        public PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress
            {
                ScoreData =
                {
                    Value = 0
                },
                BusinessNodes = new List<BusinessNodeData>
                {
                    CreateDefaultBusinessNodeData(BusinessTypeId.Business1),
                    CreateDefaultBusinessNodeData(BusinessTypeId.Business2),
                    CreateDefaultBusinessNodeData(BusinessTypeId.Business3),
                    CreateDefaultBusinessNodeData(BusinessTypeId.Business4),
                    CreateDefaultBusinessNodeData(BusinessTypeId.Business5)
                }
            };

            return progress;
        }

        private BusinessNodeData CreateDefaultBusinessNodeData(BusinessTypeId id)
        {
            var businessStaticData = _staticDataService.GetBusinessStaticData(id);
            var businessId = businessStaticData.Id;
            var defaultLevel = businessStaticData.DefaultLevel;
            var defaultPrice = businessStaticData.DefaultPrice;
            var defaultIncome = businessStaticData.DefaultIncome;

            var upgradeDataList = CreateDefaultUpgradeData(id);

            return new BusinessNodeData
            {
                Id = businessId,
                Level = defaultLevel,
                LevelUpPrice = defaultPrice,
                Income = defaultIncome,
                Upgrades = upgradeDataList
            };
        }

        private List<UpgradeData> CreateDefaultUpgradeData(BusinessTypeId id)
        {
            var upgradeStaticDataList = _staticDataService.GetUpgradeStaticDataList(id);
            var upgradeDataList = new List<UpgradeData>();
            foreach (var upgradeStaticData in upgradeStaticDataList)
            {
                var upgradeData = new UpgradeData
                {
                    UpgradeId = upgradeStaticData.UpgradeId,
                    BusinessId = upgradeStaticData.BusinessId,
                    Unlocked = false
                };
                upgradeDataList.Add(upgradeData);
            }

            upgradeDataList.Reverse();
            
            return upgradeDataList;
        }
    }
}