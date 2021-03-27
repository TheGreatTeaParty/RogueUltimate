using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.Instance.Play("UIclick");
    }
}
