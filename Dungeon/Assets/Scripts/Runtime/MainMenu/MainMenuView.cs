using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuView : UiScreen
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    
    [SerializeField] private float _fadeTime = 0.5f;
    
    public event Action OnPlayButtonPressed;
    public event Action OnQuitButtonPressed;
    
    private CanvasGroup _mainMenuGroup;
    
    public override void Initialize()
    {
        _mainMenuGroup = GetComponent<CanvasGroup>();
        _mainMenuGroup.alpha = 0;
    }

    public override async void Show()
    {
        await _mainMenuGroup
            .DOFade(1, _fadeTime)
            .WithCancellation(Application.exitCancellationToken);
        
        _playButton.onClick.AddListener(PlayButtonPressed);
        _quitButton.onClick.AddListener(QuitButtonPressed);
    }

    public override void Hide()
    {
        _playButton.onClick.RemoveListener(PlayButtonPressed);
        _quitButton.onClick.RemoveListener(QuitButtonPressed);
        
        _mainMenuGroup
            .DOFade(0, _fadeTime);
    }

    public void DisableButtons()
    {
        _playButton.interactable = false;
        _quitButton.interactable = false;
    }
    
    private void PlayButtonPressed() => OnPlayButtonPressed?.Invoke();
    private void QuitButtonPressed() => OnQuitButtonPressed?.Invoke();
}