using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPanelManager : MonoBehaviour
{
    private GameObject _currentWindow;
    private Animator _animator;

    public GameObject navigator;
    public GameObject inventoryPanel;
    public GameObject statsPanel;
    public GameObject skillsPanel;
    public GameObject questPanel;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeWindow(WindowType window)
    {
        switch (window)
        {
            case WindowType.Inventory:
            {
                _currentWindow?.SetActive(false);
                _currentWindow = inventoryPanel;
                _currentWindow.SetActive(true);
                break;
            }

            case WindowType.Stats:
            {
                _currentWindow?.SetActive(false);
                _currentWindow = statsPanel;
                _currentWindow.SetActive(true);
                break;
            }
            
            case WindowType.SkillTree:
            {
                _currentWindow?.SetActive(false);
                _currentWindow = skillsPanel;
                _currentWindow.SetActive(true);
                break;
            }
            
            case WindowType.QuestBook:
            {
                _currentWindow?.SetActive(false);
                _currentWindow = questPanel;
                _currentWindow.SetActive(true);
                break;
            }
            
            case WindowType.ReturnOption:
            {
                InterfaceManager.Instance.ClosePlayerPanel();
                break;
            }
        }
        
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

public enum WindowType
{
    Inventory = 0,
    Stats = 1, 
    SkillTree = 2,
    QuestBook = 3,
    ReturnOption = 4,
    None = 5
}
