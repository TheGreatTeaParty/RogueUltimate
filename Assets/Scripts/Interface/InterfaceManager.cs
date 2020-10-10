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
    public GameObject quickSlotPanel;
    [Space]

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

    public void ShowQuickSlotPanel()
    {
        
    }

    public void HideQuickSlotPanel()
    {
        
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
        HidePlayerPanel();
        HideFaceElements();
    }

}
