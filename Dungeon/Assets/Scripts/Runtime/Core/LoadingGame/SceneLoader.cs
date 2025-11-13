using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : ISceneLoader
{
    public async UniTask Load(SceneNames name)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(name.ToString());

        await handle.Task.AsUniTask();
        await handle.Result.ActivateAsync();
    }
}