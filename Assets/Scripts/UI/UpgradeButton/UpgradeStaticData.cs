using BusinessNode;
using UnityEngine;

namespace UI.UpgradeButton
{
    [CreateAssetMenu(fileName = "PowerUpStaticData", menuName = "Static Data/Power Up")]
    public class UpgradeStaticData : ScriptableObject
    {
        public UpgradeId Id;
        public BusinessTypeId BusinessId;
        public string Name;

        public string IncomeLabel = "Income";
        public string PriceLabel = "Price";
        public string SoldoutLabel = "Bought";

        public int IncomeMultiplierPercent;
        public int Price;
    }
}