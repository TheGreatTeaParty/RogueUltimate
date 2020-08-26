using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceOnScene : MonoBehaviour
{
    public static InterfaceOnScene instance;

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

}
