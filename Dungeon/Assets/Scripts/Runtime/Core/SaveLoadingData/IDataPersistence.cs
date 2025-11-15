public interface IDataPersistence
{
    void Load(GameData gameData);
    void Save(GameData gameData);
}