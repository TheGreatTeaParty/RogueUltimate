using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] public FixedJoystick fixedJoystick;
    [SerializeField] public JoystickAttack joystickAttack;
    [SerializeField] public PlayerButtonCallBack playerButton;


    private void Start()
    {
        playerPanelManager = GetComponentInChildren<PlayerPanelManager>();
        playerPanelManager.ClosePanel += HidePlayerPanel;
        playerButton.onStateChanged += ShowPlayerPanel;

        // Cache
        fixedJoystick = Instance.GetComponentInChildren<FixedJoystick>();
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

    public void HideAll()
    {
        // Order matters
        HidePlayerPanel();
        HideQuickPanel();
        HideFaceElements();
    }

}
