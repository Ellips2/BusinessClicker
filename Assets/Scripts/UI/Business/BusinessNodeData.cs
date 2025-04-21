using System;
using System.Collections.Generic;
using Business;
using UI.UpgradeButton;

namespace UI.Business
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