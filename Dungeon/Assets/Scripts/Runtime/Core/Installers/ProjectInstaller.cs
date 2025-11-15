using Zenject;

public class ProjectInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        BindSaveLoadService();
        
        BindAssetProvider();

        BindStatesFactory();
        
        BindSceneLoader();
        
        BindGameStateMachine();
    }

    private void BindSaveLoadService()
    {
        Container
            .BindInterfacesAndSelfTo<SaveLoadService>()
            .AsSingle();
    }

    private void BindSceneLoader()
    {
        Container
            .BindInterfacesAndSelfTo<SceneLoader>()
            .AsSingle();
    }

    private void BindStatesFactory()
    {
        Container
            .BindInterfacesAndSelfTo<StatesFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void BindGameStateMachine()
    {
        Container
            .BindInterfacesAndSelfTo<GameStateMachine>()
            .AsSingle()
            .NonLazy();
    }

    private void BindAssetProvider()
    {
        Container
            .BindInterfacesAndSelfTo<AssetProvider>()
            .AsSingle()
            .NonLazy();
    }
}