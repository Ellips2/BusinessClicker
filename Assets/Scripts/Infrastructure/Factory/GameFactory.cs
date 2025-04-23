using Logic.Score;

namespace Infrastructure.Factory
{
    internal class GameFactory : IGameFactory
    {
        private readonly IScoreFactory _scoreFactory;
        private readonly IBusinessNodeFactory _businessNodeFactory;

        public Score Score { get; private set; }

        public GameFactory(IScoreFactory scoreFactory, IBusinessNodeFactory businessNodeFactory)
        {
            _scoreFactory = scoreFactory;
            _businessNodeFactory = businessNodeFactory;
        }

        public void CreateScore() => Score = _scoreFactory.CreateScore();

        public void CreateBusinessNodes() => _businessNodeFactory.CreateBusinessNodes();
    }
}