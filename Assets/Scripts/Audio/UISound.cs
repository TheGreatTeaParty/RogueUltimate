using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.Instance.Play("UIclick");
    }
}
