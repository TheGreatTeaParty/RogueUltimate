using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceOnScene : MonoBehaviour
{
    public static InterfaceOnScene Instance;
    [SerializeField] private GameObject[] interfaceElements;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Hide()
    {
        foreach (var e in interfaceElements)
            e.SetActive(false);
    }
    
    public void Show()
    {
        foreach (var e in interfaceElements)
            e.SetActive(true);
    }
    
}
