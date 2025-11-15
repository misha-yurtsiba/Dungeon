using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class UiManager : MonoBehaviour
{
    [SerializeField] private UiScreen[] _screens;
    
    private Dictionary<Type, UiScreen> _compositeScreensDict = new();
    private Stack<UiScreen> _screenStack = new ();

    public void Initialize()
    {
        foreach (UiScreen screen in _screens)
        {
            _compositeScreensDict.TryAdd(screen.GetType(), screen);
            screen.Initialize();
            screen.gameObject.SetActive(false);
        }
        
        OpenScreen<MainMenuView>();
    }

    public void OpenScreen<T>() where T : UiScreen
    {
        if (_screenStack.Contains(_compositeScreensDict[typeof(T)])) return;
        
        if (_screenStack.Count > 0)
        {
            UiScreen screen = _screenStack.Peek();
            screen.gameObject.SetActive(false);
            screen.Hide();
        }
        
        _screenStack.Push(_compositeScreensDict[typeof(T)]);
        _compositeScreensDict[typeof(T)].gameObject.SetActive(true);
        _compositeScreensDict[typeof(T)].Show();
    }

    public void CloseCurrentScreen()
    {
        if (_screenStack.Count > 0)
        {
            UiScreen screen = _screenStack.Pop();
            screen.gameObject.SetActive(false);
            screen.Hide();
            
            if (_screenStack.Count > 0)
            {
                UiScreen screen1 = _screenStack.Peek();
                screen1.gameObject.SetActive(true);
                screen1.Show();
            }
        }
    }

    public T GetScreen<T>() where T : UiScreen
    {
        return _compositeScreensDict[typeof(T)] as T;
    }
}