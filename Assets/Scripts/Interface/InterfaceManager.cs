﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    #region Singleton
    
    public static InterfaceManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    #endregion


    public PlayerPanelManager playerPanelManager;
    public GameObject faceElements;
    public GameObject quickPanel;
    [Space]
    [Range(0.05f, 1f)]
    public float panelTimeScale;
    
    // Cache
    [SerializeField] public DynamicJoystick fixedJoystick;
    [SerializeField] public JoystickAttack joystickAttack;
    [SerializeField] public PlayerButtonCallBack playerButton;

    public delegate void OnChangeMade();
    public OnChangeMade HighlightNavButton;

    public delegate void OnPanelHighlight(WindowType window);
    public OnPanelHighlight HighlightPanelButton;

    //UI elements that should be off while in tavern:
    public GameObject Bars;
    public GameObject TavernUI;
    public GameObject QuickSlots;

    private void Start()
    {
        playerPanelManager = GetComponentInChildren<PlayerPanelManager>();
        playerPanelManager.ClosePanel += HidePlayerPanel;
        playerButton.onStateChanged += ShowPlayerPanel;

        StartCoroutine(Init());

        // Cache
        fixedJoystick = Instance.GetComponentInChildren<DynamicJoystick>();

        ShowTavernUI();
    }
    
    private IEnumerator Init()
    {
        quickPanel.SetActive(true);

        yield return new WaitForSeconds(0.01f);
        
        quickPanel.SetActive(false);

    }
    
    public void DisableView()
	{
        // TODO: disable joystick sprite. 
	}

    public void ShowPlayerPanel()
    {
        HideFaceElements();
        playerPanelManager.OpenSelf();
    }

    public void HidePlayerPanel()
    {
        playerPanelManager.CloseSelf();
        ShowFaceElements();
    }

    public void ShowQuickPanel()
    {
        Time.timeScale = panelTimeScale;
        HideFaceElements();
        quickPanel.SetActive(true);   
    }

    public void HideQuickPanel()
    {
        quickPanel.SetActive(false);
        ShowFaceElements();
        Time.timeScale = 1f;
    }
    
    public void ShowFaceElements()
    {
        faceElements.SetActive(true);    
    }

    public void HideFaceElements()
    {
        faceElements.SetActive(false);
    }

    public void HideSkillSlots()
    {
        QuickSlots.SetActive(false);
    }
    public void ShowSkillSlots()
    {
        QuickSlots.SetActive(true);
    }
    public void HideAll()
    {
        // Order matters
        HidePlayerPanel();
        HideQuickPanel();
        HideFaceElements();
    }
    public void ShowAll()
    {
        // Order matters
        ShowPlayerPanel();
        ShowQuickPanel();
        ShowFaceElements();
    }

    public void ShowTavernUI()
    {
        QuickSlots.SetActive(false);
        Bars.SetActive(false);
        TavernUI.SetActive(true);
    }

    public void HideTavernUI()
    {
        fixedJoystick.ResetInput();
        QuickSlots.SetActive(true);
        Bars.SetActive(true);
        TavernUI.SetActive(false);
    }
}
