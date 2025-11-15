using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LoadingGameplayState : IPayload<SceneNames>
{
    private readonly SceneLoader _sceneLoader;
    private readonly AssetProvider _assetProvider;
    private readonly GameStateMachine _stateMachine;
    private readonly ISaveLoadService _saveLoadService;

    private LoadingCurtain _loadingCurtain;
    
    public LoadingGameplayState(SceneLoader sceneLoader, AssetProvider assetProvider, GameStateMachine stateMachine, ISaveLoadService saveLoadService)
    {
        _sceneLoader = sceneLoader;
        _assetProvider = assetProvider;
        _stateMachine = stateMachine;
        _saveLoadService = saveLoadService;
    }
    
    public async void Enter(SceneNames payload)
    {
        _loadingCurtain = await _assetProvider.InstantiateAssetAsync<LoadingCurtain>(AssetsPath.LoadingCurtain);
        Object.DontDestroyOnLoad(_loadingCurtain.gameObject);
        
        Debug.Log($"Save existed {_saveLoadService.IsSaveExists()}");
        
        await _loadingCurtain.Show();
        await _sceneLoader.Load(payload);
        await UniTask.Yield();
        _saveLoadService.LoadData();
        GameplayRunner runner = Object.FindFirstObjectByType<GameplayRunner>();
        await UniTask.Yield();
        await _loadingCurtain.Hide();
        
        runner.Run();
        _stateMachine.Enter<GameplayState>();
    }

    public void Exit()
    {
        _assetProvider.UnloadInstance(_loadingCurtain.gameObject);
        _loadingCurtain = null;
    }
}