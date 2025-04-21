namespace SaveLoad
{
    public interface ISaveLoadService
    {
        void Save(PlayerProgress progress);
        PlayerProgress Load();
        PlayerProgress NewProgress();
    }
}