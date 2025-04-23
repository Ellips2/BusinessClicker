using System.Collections.Generic;
using Logic.Business;
using Logic.Score;
using Logic.UpgradeButton;

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