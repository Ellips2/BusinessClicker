using Logic.Business;
using UnityEngine;

namespace Logic.UpgradeButton
{
    [CreateAssetMenu(fileName = "UpgradeStaticData", menuName = "Static Data/Upgrade")]
    public class UpgradeStaticData : ScriptableObject
    {
        public UpgradeTypeId UpgradeId;
        public BusinessTypeId BusinessId;
        public string Name;

        public string IncomeLabel = "Income";
        public string PriceLabel = "Price";
        public string SoldoutLabel = "Bought";

        public int IncomeMultiplierPercent;
        public int Price;
    }
}