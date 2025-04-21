using System;
using Business;

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