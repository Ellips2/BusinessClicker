using System.Collections.Generic;
using Business;
using UI.Business;
using UI.Score;
using UI.UpgradeButton;

namespace Services
{
    public interface IStaticDataService
    {
        void Load();
        BusinessStaticData ForBusiness(BusinessTypeId businessCardId);
        ScoreStaticData ForScore();
        UpgradeStaticData ForUpgrade(UpgradeId upgradeId);
        List<UpgradeStaticData> ForUpgrades(BusinessTypeId businessTypeId);
    }
}