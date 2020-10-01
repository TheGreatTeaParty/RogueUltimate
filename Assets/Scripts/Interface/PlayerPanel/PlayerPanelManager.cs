using System;
using UnityEngine;
using UnityEngine.UI;


public enum WindowType
{
    Inventory = 0,
    Stats = 1, 
    SkillTree = 2,
    QuestBook = 3,
    ReturnOption = 4,
    None = 5
}


public class PlayerPanelManager : MonoBehaviour
{
    private GameObject _currentWindow;
    private Animator _animator;
    private NavigatorButton _currentNavigatorButton;
    private NavigatorButton[] _navigatorButtons;
    
    public GameObject navigator;
    public GameObject inventoryPanel;
    public GameObject statsPanel;
    public GameObject skillsPanel;
    public GameObject questPanel;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _navigatorButtons = navigator.GetComponentsInChildren<NavigatorButton>();
        for (int i = 0; i < _navigatorButtons.Length; i++)
            _navigatorButtons[i].onWindowChanged += ChangeWindow;
    }

    public void ChangeWindow(WindowType window, NavigatorButton navigatorButton)
    {
        switch (window)
        {
            case WindowType.Inventory:
            {
                SwitchWindow(inventoryPanel);
                HighlightNavButton(navigatorButton);
                break;
            }

            case WindowType.Stats:
            {
                SwitchWindow(statsPanel);
                HighlightNavButton(navigatorButton);
                break;
            }
            
            case WindowType.SkillTree:
            {
                SwitchWindow(skillsPanel);
                HighlightNavButton(navigatorButton);
                break;
            }
            
            case WindowType.QuestBook:
            {
                SwitchWindow(questPanel);
                HighlightNavButton(navigatorButton);
                break;
            }
            
            case WindowType.ReturnOption:
            {
                HighlightNavButton(null);
                InterfaceManager.Instance.ClosePlayerPanel();
                break;
            }
        }
        
    }

    private void SwitchWindow(GameObject window)
    {
        _currentWindow?.SetActive(false);
        _currentWindow = window;
        _currentWindow.SetActive(true);
    }

    private void HighlightNavButton(NavigatorButton navigatorButton)
    {
        _currentNavigatorButton?.Highlight(false);
        _currentNavigatorButton = navigatorButton;
        _currentNavigatorButton?.Highlight(true);
    } 
    
    public void OpenSelf()
    {
        Time.timeScale = 0.2f;
        _animator.SetFloat("Kind of animation", 0f);
        navigator.SetActive(true);
    }

    public void CloseSelf()
    {
        _currentWindow?.SetActive(false);
        _currentWindow = null;
        navigator.SetActive(false);
        
        Time.timeScale = 1f;
        _animator.SetFloat("Kind of animation", 1f);
    }

}  
