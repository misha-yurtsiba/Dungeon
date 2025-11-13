using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private GameStateMachine _stateMachine;
    private StatesFactory _statesFactory;
    
    [Inject]
    private void Construct(GameStateMachine stateMachine, StatesFactory statesFactory)
    {
        _stateMachine = stateMachine;
        _statesFactory = statesFactory;
    }
    private void Awake()
    {
        _stateMachine.AddState<BootstrapState>(_statesFactory.Create<BootstrapState>());
        
        _stateMachine.Enter<BootstrapState>();
    }
}

