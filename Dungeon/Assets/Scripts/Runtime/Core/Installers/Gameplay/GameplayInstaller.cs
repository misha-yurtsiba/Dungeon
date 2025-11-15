using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private GameplayRunner _gameplayRunner;
    public override void InstallBindings()
    {
        BindGameplayRunner();
    }

    private void BindGameplayRunner()
    {
        Container
            .Bind<GameplayRunner>()
            .FromInstance(_gameplayRunner)
            .AsSingle();
    }
}
