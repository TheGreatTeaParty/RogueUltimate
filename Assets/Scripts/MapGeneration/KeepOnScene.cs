using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnScene : MonoBehaviour
{
    public static KeepOnScene instance;

    private void Awake()
    {
        if(instance == null)
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
