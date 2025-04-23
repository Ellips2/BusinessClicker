namespace SaveLoad
{
    public interface ISaveLoadService
    {
        void Save(PlayerProgress progress);
        PlayerProgress LoadProgress();
        PlayerProgress NewProgress();
    }
}