﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBack : MonoBehaviour,IInteractable
{
    public Transform VictoryPanel;

    public string GetActionName()
    {
        return "to tavern";
    }

    public void Interact()
    {
        var UI = InterfaceManager.Instance;
        UI.HideAll();
        Instantiate(VictoryPanel);
        Destroy(this);
    }
}
