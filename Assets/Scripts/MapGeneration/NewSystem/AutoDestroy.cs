using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        Invoke("TriggerDestration", 0.1f);
    }
    public void TriggerDestration()
    {
        Destroy(gameObject);
    }
}
