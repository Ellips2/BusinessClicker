using UI.Business;

namespace UI.UpgradeButton
{
    public struct Upgrade
    {
        public UpgradeTypeId TypeId;
        public BusinessTypeId BusinessId;
        public bool Unlocked;
        public int Price;
        public float IncomeMultiplierPercent;
    }
}