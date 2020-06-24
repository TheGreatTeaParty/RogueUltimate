using System;
using System.Collections.Generic;
using UnityEngine;


public class QuickSlotsManager : MonoBehaviour 
{
    #region Singleton

    public static QuickSlotsManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion

    public List<QuickSlot> slots;

}