using TMPro;
using UnityEngine;

namespace UI.UpgradeButton
{
    internal class UpgradeView : MonoBehaviour
    {
        public UpgradeId Id;
        public TextMeshProUGUI Name;

        public RuleTile.TilingRuleOutput.Transform IncomeTransform;
        public TextMeshProUGUI IncomeLabel;
        public TextMeshProUGUI IncomeValue;

        public Transform PriceTransform;
        public TextMeshProUGUI PriceLabel;
        public TextMeshProUGUI PriceValue;

        public TextMeshProUGUI SoldoutLabel;
    }
}