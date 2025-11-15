using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly StatesFactory _statesFactory;

    public BootstrapState(GameStateMachine stateMachine, StatesFactory statesFactory)
    {
        _stateMachine = stateMachine;
        _statesFactory = statesFactory;
    }

    public void Enter()
    {
        Application.targetFrameRate = 60;
        
        _stateMachine.AddState<LoadingMenuState>(_statesFactory.Create<LoadingMenuState>());
        _stateMachine.AddState<MainMenuState>(_statesFactory.Create<MainMenuState>());
        _stateMachine.AddState<LoadingGameplayState>(_statesFactory.Create<LoadingGameplayState>());
        _stateMachine.AddState<GameplayState>(_statesFactory.Create<GameplayState>());
        
        _stateMachine.Enter<LoadingMenuState, SceneNames>(SceneNames.MainMenu);
    }
    
    public void Exit()
    {
        
    }
}