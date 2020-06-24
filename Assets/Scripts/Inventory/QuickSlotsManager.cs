using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField] private UsableItem[] items;
    [SerializeField] private Button[] slots;
    private int _index = 0;
    

    public void AddItemToQuickSlot(UsableItem usableItem)
    {
        items[_index] = usableItem;
    }
    
    
}