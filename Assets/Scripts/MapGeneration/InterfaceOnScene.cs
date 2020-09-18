using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceOnScene : MonoBehaviour
{
    public static InterfaceOnScene Instance;
    [SerializeField] private GameObject[] mainElements;
    [SerializeField] private GameObject[] panels;
    
    // Cache
    [HideInInspector] public FixedJoystick fixedJoystick;
    [HideInInspector] public JoystickAttack joystickAttack;
    
    
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
        
        // Cache
        fixedJoystick = Instance.GetComponentInChildren<FixedJoystick>();
        joystickAttack = Instance.GetComponentInChildren<JoystickAttack>();
    }

    public void HideMainElements()
    {
        foreach (var e in mainElements)
            e.SetActive(false);
    }
    
    public void ShowMainElements()
    {
        foreach (var e in mainElements)
            e.SetActive(true);
    }

    public void HideAll()
    {
        foreach (var p in panels)
            p.SetActive(false);
       
        foreach (var e in mainElements)
            e.SetActive(false);
    }

}
