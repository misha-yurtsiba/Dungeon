public interface ISaveLoadService
{
    void SaveData();
    void LoadData();
    bool IsSaveExists();
    void Register(IDataPersistence dataPersistence);
    void Unregister(IDataPersistence dataPersistence);
    void UnregisterAll();
}