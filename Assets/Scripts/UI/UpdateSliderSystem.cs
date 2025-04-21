using System;
using System.Collections.Generic;
using System.Linq;
using Income;
using Leopotam.EcsLite;
using UI.Business;

namespace UI
{
    public sealed class UpdateSliderSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly UiRoot _uiRoot;
        private readonly EcsWorld _world;
        private EcsFilter _businessNodeFilter;
        private List<BusinessNodeView> _businessNodeViewList;
        private EcsPool<IncomeTimer> _incomeTimerPool;

        public UpdateSliderSystem(EcsWorld world, UiRoot uiRoot)
        {
            _world = world;
            _uiRoot = uiRoot;
        }

        public void Init(IEcsSystems systems)
        {
            _businessNodeFilter = _world.Filter<BusinessNode>().Inc<IncomeTimer>().End();
            _incomeTimerPool = _world.GetPool<IncomeTimer>();
            _businessNodeViewList = _uiRoot.GetComponentsInChildren<BusinessNodeView>().ToList();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var index in _businessNodeFilter)
            {
                ref var incomeTimer = ref _incomeTimerPool.Get(index);
                ref var businessNode = ref _world.GetPool<BusinessNode>().Get(index);

                if (businessNode.Level <= 0) return;

                foreach (var businessNodeView in _businessNodeViewList)
                    if (businessNodeView.Id == businessNode.Id)
                        businessNodeView.Slider.value = CalculateSliderValue(incomeTimer, businessNode);
            }
        }

        private float CalculateSliderValue(IncomeTimer incomeTimer, BusinessNode businessNode)
        {
            return Math.Abs(incomeTimer.Timer - businessNode.IncomeDelay) / businessNode.IncomeDelay;
        }
    }
}