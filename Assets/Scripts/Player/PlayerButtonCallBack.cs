﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonCallBack : MonoBehaviour
{
    public delegate void OnButtonCallback();

    //Deligate
    public OnButtonCallback onStateChanged;

    public void ChangeState()
    {
        onStateChanged?.Invoke();
    }
}