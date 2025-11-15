using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuUiManager _menuUiManager;
    
    public override void InstallBindings()
    {
        BindUiManager();
    }

    private void BindUiManager()
    {
        Container
            .Bind<MainMenuUiManager>()
            .FromInstance(_menuUiManager)
            .AsSingle();
    }
}