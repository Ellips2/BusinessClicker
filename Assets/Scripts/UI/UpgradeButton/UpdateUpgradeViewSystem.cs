using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using GameWorld;
using UI;
using UI.Markers;
using UI.Score;

namespace UI.UpgradeButton
{
    public sealed class UpdateUpgradeViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameFactory _factory;
        private readonly UiRoot _uiRoot;
        private readonly EcsWorld _world;
        private ScoreView _balanceView;
        private EcsFilter _filter;
        private EcsPool<Upgrade> _upgradePool;
        private List<UpgradeView> _upgradeViewList;

        public UpdateUpgradeViewSystem(EcsWorld world, IGameFactory factory, UiRoot uiRoot)
        {
            _world = world;
            _factory = factory;
            _uiRoot = uiRoot;
        }

        public void Init(IEcsSystems systems)
        {
            _filter = _world.Filter<Upgrade>().Inc<Unlocked>().End();
            _balanceView = _uiRoot.ScorePanel.GetComponentInChildren<ScoreView>();
            _upgradeViewList = _uiRoot.GetComponentsInChildren<UpgradeView>().ToList();
            _upgradePool = _world.GetPool<Upgrade>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _filter)
            {
                ref var upgrade = ref _upgradePool.Get(index);

                foreach (var upgradeView in _upgradeViewList)
                    if (upgradeView.UpgradeId == upgrade.TypeId)
                    {
                        upgradeView.IncomeTransform.gameObject.SetActive(false);
                        upgradeView.PriceTransform.gameObject.SetActive(false);
                        upgradeView.SoldoutLabel.gameObject.SetActive(true);
                    }

                _balanceView.Value.text = _factory.Score.Value.ToString();
            }
        }
    }
}