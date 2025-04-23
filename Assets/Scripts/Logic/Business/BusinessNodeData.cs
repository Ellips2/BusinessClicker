using System;
using System.Collections.Generic;
using Logic.UpgradeButton;

namespace Logic.Business
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