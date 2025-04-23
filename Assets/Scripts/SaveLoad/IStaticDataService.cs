using System.Collections.Generic;
using UI.Business;
using UI.Score;
using UI.UpgradeButton;

namespace SaveLoad
{
    public interface IStaticDataService
    {
        void LoadStaticData();
        BusinessStaticData GetBusinessStaticData(BusinessTypeId businessNodeId);
        ScoreStaticData GetScoreStaticData();
        UpgradeStaticData GetUpgradeStaticData(UpgradeTypeId upgradeTypeId);
        List<UpgradeStaticData> GetUpgradeStaticDataList(BusinessTypeId businessTypeId);
    }
}