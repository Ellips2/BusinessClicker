using System;
using BusinessNode;

namespace UI.UpgradeButton
{
    [Serializable]
    public struct UpgradeData
    {
        public UpgradeId Id;
        public BusinessTypeId BusinessId;
        public bool Unlocked;
    }
}