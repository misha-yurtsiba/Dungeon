using Zenject;

public class MainMenuPresenterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMainMenuPresenter();
    }

    private void BindMainMenuPresenter()
    {
        Container
            .BindInterfacesAndSelfTo<MainMenuPresenter>()
            .AsSingle();
    }
}