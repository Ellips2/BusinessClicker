using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UI.Business;
using UI.LevelUpButton;
using UI.Markers;
using UI.UpgradeButton;
using UnityEngine.Scripting;

namespace UI
{
    internal sealed class UiClickEventSystem : EcsUguiCallbackSystem
    {
        private readonly EcsFilter _businessNodeFilter;
        private readonly EcsPool<BusinessNode> _businessNodePool;
        private readonly EcsFilter _clickedBusinessNode;
        private readonly EcsPool<Clicked> _clickedPool;
        private readonly EcsFilter _clickedUpgrade;
        private readonly EcsFilter _upgradeFilter;
        private readonly EcsPool<Upgrade> _upgradePool;
        private readonly EcsWorld _world;

        public UiClickEventSystem(EcsWorld world)
        {
            _world = world;

            _upgradeFilter = _world.Filter<Upgrade>().End();
            _clickedUpgrade = _world.Filter<Upgrade>().Inc<Clicked>().End();

            _upgradeFilter = _world.Filter<Upgrade>().End();
            _businessNodeFilter = _world.Filter<BusinessNode>().End();
            _clickedBusinessNode = _world.Filter<BusinessNode>().Inc<Clicked>().End();
            _clickedUpgrade = _world.Filter<Upgrade>().Inc<Clicked>().End();

            _upgradePool = _world.GetPool<Upgrade>();
            _businessNodePool = _world.GetPool<BusinessNode>();
            _clickedPool = _world.GetPool<Clicked>();
        }

        [Preserve]
        [EcsUguiClickEvent]
        private void OnAnyClick(in EcsUguiClickEvent e)
        {
            SetBusinessNodeLevelUpClicked(e);
            SetUpgradeClicked(e);

            SendUpgradeBoughtEvent(e);
            SendBusinessNodeLevelUpBoughtEvent(e);
        }

        private void SendBusinessNodeLevelUpBoughtEvent(EcsUguiClickEvent e)
        {
            foreach (var index in _clickedBusinessNode)
            {
                e.Sender.TryGetComponent(out LevelUpView levelUpView);

                var businessNodeView = levelUpView.BusinessNodeView;

                ref var buyLevelUpEvent = ref _world.GetPool<BuyLevelUpEvent>().Add(index);

                buyLevelUpEvent.Id = businessNodeView.Id;
            }
        }

        private void SendUpgradeBoughtEvent(EcsUguiClickEvent e)
        {
            foreach (var index in _clickedUpgrade)
            {
                var upgradeView = e.Sender.GetComponent<UpgradeView>();

                ref var buyUpgradeEvent = ref _world.GetPool<BuyUpgradeEvent>().Add(index);

                buyUpgradeEvent.TypeId = upgradeView.UpgradeId;
            }
        }

        private void SetUpgradeClicked(EcsUguiClickEvent e)
        {
            e.Sender.TryGetComponent(out UpgradeView upgradeView);

            if (upgradeView == null)
                return;

            foreach (var index in _upgradeFilter)
            {
                ref var entityUpgrade = ref _upgradePool.Get(index);

                if (entityUpgrade.Unlocked)
                    continue;

                if (upgradeView.UpgradeId == entityUpgrade.TypeId) _clickedPool.Add(index);
            }
        }

        private void SetBusinessNodeLevelUpClicked(EcsUguiClickEvent e)
        {
            e.Sender.TryGetComponent(out LevelUpView levelUpView);

            if (levelUpView == null)
                return;

            var businessNodeView = levelUpView.BusinessNodeView;

            if (businessNodeView == null)
                return;

            foreach (var index in _businessNodeFilter)
            {
                ref var entityBusinessNode = ref _businessNodePool.Get(index);
                if (businessNodeView.Id == entityBusinessNode.Id) _clickedPool.Add(index);
            }
        }
    }
}