using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceOnScene : MonoBehaviour
{
    public static InterfaceOnScene instance;
    [SerializeField] private GameObject[] interfaceElements;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
