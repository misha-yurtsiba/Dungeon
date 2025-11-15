using UnityEngine;
using Zenject;

public class MainMenuViewInstaller : MonoInstaller
{
    [SerializeField] private MainMenuView _mainMenuView;
    
    public override void InstallBindings()
    {
        BindMainMenuView();
    }

    private void BindMainMenuView()
    {
        Container
            .Bind<MainMenuView>()
            .FromInstance(_mainMenuView)
            .AsSingle();
    }
}