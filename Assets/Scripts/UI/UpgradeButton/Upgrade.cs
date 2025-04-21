using Business;

namespace UI.UpgradeButton
{
    public struct Upgrade
    {
        public UpgradeId Id;
        public BusinessTypeId BusinessId;
        public bool Unlocked;
        public int Price;
        public float IncomeMultiplierPercent;
    }
}