using System.Collections.Generic;
using UI.Business;
using UI.Score;
using UI.UpgradeButton;

namespace SaveLoad
{
    public interface IStaticDataService
    {
        void Load();
        BusinessStaticData ForBusiness(BusinessTypeId businessNodeId);
        ScoreStaticData ForScore();
        UpgradeStaticData ForUpgrade(UpgradeId upgradeId);
        List<UpgradeStaticData> ForUpgrades(BusinessTypeId businessTypeId);
    }
}