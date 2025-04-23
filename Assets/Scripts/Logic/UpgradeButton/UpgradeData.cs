using System;
using Logic.Business;

namespace Logic.UpgradeButton
{
    [Serializable]
    public struct UpgradeData
    {
        public UpgradeTypeId UpgradeId;
        public BusinessTypeId BusinessId;
        public bool Unlocked;
    }
}