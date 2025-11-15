using System;
using UnityEngine;
using Zenject;

public class TestSave : MonoBehaviour, IDataPersistence
{
    private ISaveLoadService _saveLoadService;

    [Inject]
    private void Construct(ISaveLoadService saveLoadService)
    {
        _saveLoadService = saveLoadService;
    }
    private void Awake()
    {
        _saveLoadService.Register(this);
    }

    private void OnDestroy()
    {
        _saveLoadService.SaveData();
        _saveLoadService.Unregister(this);
    }

    public void Load(GameData gameData)
    {
        transform.position = gameData.position;
    }

    public void Save(GameData gameData)
    {
        gameData.position = transform.position;
    }
}
