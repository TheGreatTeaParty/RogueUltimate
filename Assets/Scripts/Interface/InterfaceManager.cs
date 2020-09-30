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
    [Space]

    // Cache
    [SerializeField] public FixedJoystick fixedJoystick;
    [SerializeField] public JoystickAttack joystickAttack;
    [SerializeField] public PlayerButtonCallBack playerButton;


    private void Start()
    {
        playerPanelManager = GetComponentInChildren<PlayerPanelManager>();
       
        playerButton.onStateChanged += OpenPlayerPanel;
        // PlayerOnScene.Instance.GetComponentInChildren<PlayerButtonCallBack>().onStateChanged += OpenPlayerPanel;
        
        // Cache
        fixedJoystick = Instance.GetComponentInChildren<FixedJoystick>();
    }
    
    
    public void OpenPlayerPanel()
    {
        HideFaceElements();
        playerPanelManager.OpenSelf();
    }

    public void ClosePlayerPanel()
    {
        playerPanelManager.CloseSelf();
        ShowFaceElements();
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
        ClosePlayerPanel();
        HideFaceElements();
    }

}
