using System.Collections.Generic;
using Logic.Business;
using Logic.UpgradeButton;

namespace Infrastructure.Factory
{
    internal interface IUpgradeFactory
    {
        void CreateUpgrades(List<UpgradeData> upgrades, BusinessNodeView view, ref BusinessNode node);
    }
}