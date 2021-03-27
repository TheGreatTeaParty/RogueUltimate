using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public void TurnOffMap()
    {
        InterfaceManager.Instance.ShowFaceElements();
    }

    public void ClickSound()
    {
        AudioManager.Instance.Play("UIclick");
    }
}
