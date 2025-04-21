using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Business
{
    public class BusinessNodeView : MonoBehaviour
    {
        public BusinessTypeId Id;

        public TextMeshProUGUI Name;
        
        public TextMeshProUGUI LevelLabel;
        public TextMeshProUGUI LevelValue;
        
        public TextMeshProUGUI IncomeLabel;
        public TextMeshProUGUI IncomeValue;
        
        public TextMeshProUGUI LevelUpPriceLabel;
        public TextMeshProUGUI LevelUpPriceValue;

        public Transform UpgradeRoot;
        public Transform LevelUpButton;
        public Slider Slider;
    }
}