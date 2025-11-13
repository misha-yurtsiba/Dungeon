using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingMenuState : IPayload<SceneNames>
{
    private readonly SceneLoader _sceneLoader;
    private readonly AssetProvider _assetProvider;
    private readonly GameStateMachine _stateMachine;
    
    private LoadingCurtain _loadingCurtain;

    public LoadingMenuState(SceneLoader sceneLoader, AssetProvider assetProvider, GameStateMachine stateMachine)
    {
        _sceneLoader = sceneLoader;
        _assetProvider = assetProvider;
        _stateMachine = stateMachine;
    }

    public async void Enter(SceneNames payload)
    {
        _loadingCurtain = await _assetProvider.InstantiateAssetAsync<LoadingCurtain>(AssetsPath.LoadingCurtain);
        Object.DontDestroyOnLoad(_loadingCurtain.gameObject);
        
        await _loadingCurtain.Show();
        await _sceneLoader.Load(payload);
        await UniTask.Yield();
        
        Object.FindFirstObjectByType<MenuRunner>().Initialize();
        
        _stateMachine.Enter<MainMenuState>();
    }

    public async void Exit()
    {
        await _loadingCurtain.Hide();
        
        _assetProvider.UnloadInstance(_loadingCurtain.gameObject);
        _loadingCurtain = null;
    }
}