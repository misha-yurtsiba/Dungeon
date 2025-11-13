using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _loadedHandles = new();
    private readonly HashSet<GameObject> _loadedInstances = new();
    
    public async UniTask<T> LoadAssetAsync<T>(string key) where T : Object
    {
        if (_loadedHandles.TryGetValue(key, out var asyncOperationHandle))
        {
            Debug.Log($"Loading asset {key}");
            return asyncOperationHandle.Result as T;
        }
        
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task.AsUniTask();
        
        if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError($"Fail load asset {key}");
            return null;
        }
        
        _loadedHandles.Add(key, handle);  
        return handle.Result;
    }

    public void UnloadAsset(string key)
    {
        if (_loadedHandles.TryGetValue(key, out var handle))
        {
            _loadedHandles.Remove(key);
            Addressables.Release(handle);
        }
        else
        {
            Debug.LogError("Fail unload asset");
        }
    }

    public async UniTask<T> InstantiateAssetAsync<T>(string key) where T : Object
    {
        AsyncOperationHandle handle = Addressables.InstantiateAsync(key);
        
        await handle.Task;
        
        _loadedInstances.Add(handle.Result as GameObject);
        return handle.Result is GameObject gameObject ? gameObject.GetComponent<T>() : null;
    }

    public void UnloadInstance(GameObject instance)
    {
        if (instance == null) return;
        
        if (_loadedInstances.Remove(instance))
        {
            Addressables.ReleaseInstance(instance);
        }
        else
        {
            Debug.LogError($"Fail unload asset {instance.name}");
        }
    }
    
    public void UnloadAll()
    {
        foreach (KeyValuePair<string, AsyncOperationHandle> pair in _loadedHandles)
        {
            Addressables.Release(pair.Value);
            Debug.Log($"Unloaded asset {pair.Key}");
        }
        _loadedHandles.Clear();

        foreach (GameObject instance in _loadedInstances)
        {
            if (instance != null)
            {
                Addressables.ReleaseInstance(instance);
                Debug.Log($"Unloaded instance {instance.name}");
            }
        }
        _loadedInstances.Clear();

        Debug.Log("All assets and instances unloaded");
    }
}