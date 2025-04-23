using System;
using UI.Business;

namespace UI.UpgradeButton
{
    [Serializable]
    public struct UpgradeData
    {
        public UpgradeTypeId UpgradeId;
        public BusinessTypeId BusinessId;
        public bool Unlocked;
    }
}