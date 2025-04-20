using System.Collections.Generic;
using BusinessNode;
using Leopotam.EcsLite;

namespace UI.BusinessNode
{
    public struct BusinessNode
    {
        public int EntityId;
        public BusinessTypeId Id;
        public int Level;
        public float Income;
        public float IncomeDelay;
        public int LevelUpPrice;
        public List<EcsPackedEntityWithWorld> Upgrades;
    }
}