using System;
using System.Collections;
using UnityEngine;


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
    private NavigatorButton _currentNavigatorButton;
    private NavigatorButton[] _navigatorButtons;

    public GameObject navigator;
    public GameObject inventoryPanel;
    public GameObject statsPanel;
    public GameObject skillsPanel;
    public GameObject questPanel;

    public event Action ClosePanel;


    private void Start()
    {
        _navigatorButtons = navigator.GetComponentsInChildren<NavigatorButton>();
        for (int i = 0; i < _navigatorButtons.Length; i++)
            _navigatorButtons[i].onWindowChanged += ChangeWindow;

        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        navigator.SetActive(true);
        inventoryPanel.SetActive(true);
        statsPanel.SetActive(true);
        skillsPanel.SetActive(true);
        questPanel.SetActive(true);

        yield return new WaitForSeconds(0.01f);
        
        navigator.SetActive(false);
        inventoryPanel.SetActive(false);
        statsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        questPanel.SetActive(false);
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
                ClosePanel?.Invoke();
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
        Time.timeScale = InterfaceManager.Instance.panelTimeScale;
        navigator.SetActive(true);
    }

    public void CloseSelf()
    {
        _currentWindow?.SetActive(false);
        _currentWindow = null;
        navigator.SetActive(false);
        
        CharacterManager.Instance.RemoveTooltip();
        
        Time.timeScale = 1f;
    }
}  
