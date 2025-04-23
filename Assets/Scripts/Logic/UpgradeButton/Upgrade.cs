using Logic.Business;

namespace Logic.UpgradeButton
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