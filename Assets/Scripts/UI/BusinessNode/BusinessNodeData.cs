using System;
using System.Collections.Generic;
using BusinessNode;
using UI.UpgradeButton;

namespace UI.BusinessNode
{
    [Serializable]
    public class BusinessNodeData
    {
        public BusinessTypeId Id;
        public int Level;
        public float Income;
        public int LevelUpPrice;
        public List<UpgradeData> Upgrades;
    }
}