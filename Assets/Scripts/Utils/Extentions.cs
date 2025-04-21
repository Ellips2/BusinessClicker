using System.Collections.Generic;
using Leopotam.EcsLite;
using SaveLoad;
using UI.Business;
using UI.Markers;
using UI.Score;
using UI.UpgradeButton;

namespace Utils
{
    public static class Extentions
    {
        public static bool HasEnoughScore(this Score score, float price)
        {
            return price <= score.Value;
        }

        public static void SpendScore(this Score score, float price)
        {
            score.Value -= price;
        }

        public static float CalculateBusinessIncome(this ref BusinessNode businessNode,
            IStaticDataService staticDataService)
        {
            var businessStaticData = staticDataService.ForBusiness(businessNode.Id);

            return businessNode.Level
                   * businessStaticData.DefaultIncome
                   * (1 + businessNode.GetUnlockedUpgradesMultiplyer());
        }

        private static float GetUnlockedUpgradesMultiplyer(this ref BusinessNode businessNode)
        {
            float upgradesMultiplyer = 0;

            var businessNodeUpgrades = UnpackUpgrades(ref businessNode);

            foreach (var upgrade in businessNodeUpgrades)
            {
                var newUpgrade = upgrade;
                if (newUpgrade.Unlocked) upgradesMultiplyer += newUpgrade.GetIncomeMultiplierPercentage();
            }


            return upgradesMultiplyer;
        }

        private static List<Upgrade> UnpackUpgrades(ref BusinessNode businessNode)
        {
            var businessNodeUpgrades = new List<Upgrade>();
            foreach (var packedEntity in businessNode.Upgrades)
            {
                packedEntity.Unpack(out var world, out var entity);
                ref var upgrade = ref world.GetPool<Upgrade>().Get(entity);
                businessNodeUpgrades.Add(upgrade);
            }

            return businessNodeUpgrades;
        }

        public static float GetIncomeMultiplierPercentage(this Upgrade upgrade)
        {
            var upgradeIncomeMultiplierPercent = 1f + upgrade.IncomeMultiplierPercent / 100f;
            return upgradeIncomeMultiplierPercent;
        }

        public static void SendUpdateComponentEvent(this EcsPool<UpdateComponentViewEvent> updateComponentViewPool,
            int entityId)
        {
            if (!updateComponentViewPool.Has(entityId))
                updateComponentViewPool.Add(entityId);
        }

        public static void SendSaveEvent(this EcsPool<SaveEvent> savePool,
            int entityId)
        {
            if (!savePool.Has(entityId))
                savePool.Add(entityId);
        }
    }
}
