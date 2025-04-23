using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Logic.Business
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