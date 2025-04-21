using UI.Score;

namespace GameWorld
{
    public interface IGameFactory
    {
        public Score Score { get; set; }
        public void CreateScore();
        public void CreateBusinessNodes();
    }
}