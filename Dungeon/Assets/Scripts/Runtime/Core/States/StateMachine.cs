using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected readonly Dictionary<Type, IExitable> _states = new();

    protected IExitable Current;
        
    public void AddState<T>(IExitable state) where T : IExitable
    {
        if (_states.ContainsKey(state.GetType()))
        {
            Debug.LogError($"State {state.GetType().Name} is already registered");
            return;
        }
        
        _states.Add(typeof(T), state);
    }
    
    public void Enter<TState>() where TState : class, IState
    {
        ChangeState<TState>().Enter();
    }
    
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayload<TPayload>
    {
        ChangeState<TState>().Enter(payload);
    }
    
    protected T ChangeState<T>() where T : class, IExitable
    {
        Current?.Exit();
        Current = _states[typeof(T)];
        
        return Current as T;
    }

    public void RemoveState<T>() where T : IExitable
    {
        if (_states.TryGetValue(typeof(T), out IExitable state))
        {
            _states.Remove(state.GetType());
        }
        else
        {
            Debug.LogError($"State {typeof(T).Name} is not registered");
        }
    }

    public void RemoveAllStates()
    {
        _states.Clear();
    }

    public void Update()
    {
        if (Current is IUpdateable updateable)
        {
            updateable.Update();
        }
    }
}