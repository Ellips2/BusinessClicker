using Logic;
using Logic.Score;
using SaveLoad;

namespace Infrastructure.Factory
{
    internal class ScoreFactory : IScoreFactory
    {
        private readonly PlayerProgress _progress;
        private readonly IStaticDataService _staticDataService;
        private readonly UiRoot _uiRoot;

        public ScoreFactory(PlayerProgress progress, IStaticDataService staticDataService, UiRoot uiRoot)
        {
            _progress = progress;
            _staticDataService = staticDataService;
            _uiRoot = uiRoot;
        }

        public Score CreateScore()
        {
            var score = new Score { Value = _progress.ScoreData.Value };
            var scoreView = _uiRoot.ScorePanel.GetComponentInChildren<ScoreView>();
        
            scoreView.ScoreLabel.text = _staticDataService.GetScoreStaticData().ScoreLabel;
            scoreView.Value.text = score.Value.ToString();
        
            return score;
        }
    }

}