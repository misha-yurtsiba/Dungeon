using System;
using UnityEngine;

public class MainMenuPresenter : IDisposable
{
    private readonly MainMenuView _view;
    private readonly GameStateMachine _stateMachine;

    public MainMenuPresenter(MainMenuView view, GameStateMachine stateMachine)
    {
        _view = view;
        _stateMachine = stateMachine;

        _view.OnPlayButtonPressed += Play;
        _view.OnQuitButtonPressed += Quit;
    }

    public void Dispose()
    {
        _view.OnPlayButtonPressed -= Play;
        _view.OnQuitButtonPressed -= Quit;
    }

    private void Play()
    {
        _view.DisableButtons();
        _stateMachine.Enter<LoadingGameplayState, SceneNames>(SceneNames.Gameplay);
    }

    private void Quit()
    {
        Application.Quit();        
    }
}