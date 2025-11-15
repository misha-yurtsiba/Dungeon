using System.Collections.Generic;
using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private readonly List<IDataPersistence> _dataPersistenceList = new ();
    private readonly ISaveLoadStrategy _saveLoadStrategy = new JsonSaveLoadStrategy();
    
    private const string _saveName = "GameData";
    
    private GameData _gameData;

    public void LoadData()
    {
        if (!IsSaveExists())
        {
            _gameData = new GameData();
        }
        else
        {
            _gameData = _saveLoadStrategy.Load<GameData>(_saveName);
        }
        
        foreach (IDataPersistence dataPersistence in _dataPersistenceList)
        {
            dataPersistence.Load(_gameData);                   
        }
    }

    public void SaveData()
    {
        _gameData ??= new GameData();
        
        foreach (IDataPersistence dataPersistence in _dataPersistenceList)
        {
            dataPersistence.Save(_gameData);                          
        }
        
        _saveLoadStrategy.Save(_saveName, _gameData);
    }

    public void Register(IDataPersistence dataPersistence)
    {
        _dataPersistenceList.Add(dataPersistence);
    }

    public void Unregister(IDataPersistence dataPersistence)
    {
        _dataPersistenceList.Remove(dataPersistence);
    }

    public void UnregisterAll()
    {
        _dataPersistenceList.Clear();
    }

    public bool IsSaveExists() => _saveLoadStrategy.IsSaveExists(_saveName);
}