using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public static Minimap instance;

    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ShowMap()
    {
        this.enabled = true;
    }
    public void HideMap()
    {
        this.enabled = false;
    }
}
