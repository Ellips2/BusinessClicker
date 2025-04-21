using UnityEngine;
using Leopotam.EcsLite.Unity.Ugui;

namespace UI
{
    public class UiRoot
    {
        public RuleTile.TilingRuleOutput.Transform businessNodeContainer;
        public GameObject scorePanel;
        public EcsUguiEmitter uiEmitter;

        private void Awake()
        {
            uiEmitter = scorePanel.GetComponent<EcsUguiEmitter>();
        }
    }
}