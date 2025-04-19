using StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents
{
    public class BusinessNode : MonoBehaviour
    {
        public BusinessTypeId Id;

        public TextMeshProUGUI Name;
        
        public TextMeshProUGUI LevelLabel;
        public TextMeshProUGUI LevelValue;
        
        public TextMeshProUGUI IncomeLabel;
        public TextMeshProUGUI IncomeValue;
        
        public TextMeshProUGUI LevelUpPriceLabel;
        public TextMeshProUGUI LevelUpPriceValue;

        public RuleTile.TilingRuleOutput.Transform UpgradeRoot;
        public Transform LevelUpButton;
        public Slider Slider;
    }
}