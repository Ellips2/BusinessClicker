using Logic.Score;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        public Score Score { get;}
        public void CreateScore();
        public void CreateBusinessNodes();
    }
}