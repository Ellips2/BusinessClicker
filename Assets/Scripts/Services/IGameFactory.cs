using UI.Score;

namespace Services
{
    public interface IGameFactory
    {
        public Score Score { get; set; }
        public void CreateScore();
        public void CreateBusinessNodes();
    }
}