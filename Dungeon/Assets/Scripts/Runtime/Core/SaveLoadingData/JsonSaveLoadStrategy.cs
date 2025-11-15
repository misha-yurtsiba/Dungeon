using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonSaveLoadStrategy : ISaveLoadStrategy
{
    public void Save<T>(string key, T data)
    {
        try
        {
            //string json = JsonConvert.SerializeObject(data);
            string json = JsonUtility.ToJson(data);
            
            File.WriteAllText(BuildPath(key), json);
            Debug.Log(BuildPath(key));
        }
        catch (Exception e)
        {
            throw new Exception(BuildPath(key), e);
        }
    }

    public T Load<T>(string key)
    {
        try
        {
            string json = File.ReadAllText(BuildPath(key));
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception e)
        {
            throw new Exception(BuildPath(key), e);
        }
    }

    public bool IsSaveExists(string key)
    {
        return File.Exists(BuildPath(key));
    }
    
    private string BuildPath(string key) => Path.Combine(Application.persistentDataPath, key);
}