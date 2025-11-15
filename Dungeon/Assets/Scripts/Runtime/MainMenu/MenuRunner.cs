using UnityEngine;
using Zenject;

public class MenuRunner : MonoBehaviour
{
    private MainMenuUiManager _mainMenuUiManager;
    
    [Inject]
    private void Construct(MainMenuUiManager mainMenuUiManager)
    {
        _mainMenuUiManager = mainMenuUiManager;
    }
    
    public void Run()
    {
        _mainMenuUiManager.Initialize();
    }
}