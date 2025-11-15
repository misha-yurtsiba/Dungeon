public interface ISaveLoadStrategy
{
    void Save<T>(string key, T data);
    T Load<T>(string key);
    bool IsSaveExists(string key);       
}