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

    [SerializeField] private List<UsableItem> items;
    [SerializeField] private QuickSlot[] slots;


    private void Start()
    {
        UpdateUI();
    }


    public void AddItemToQuickSlot(UsableItem usableItem)
    {
        items.Add(usableItem);
        UpdateUI();
    }


    public void UseItem(UsableItem usableItem)
    {
        if (usableItem != null)
        {
            usableItem.Use();
            items.Remove(usableItem);
            UpdateUI();
        }
    }
    
    
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count) slots[i].AddItemToQuickAccessSlot(items[i]);
            else slots[i].RemoveItemFromQuickAccessSlot();
        }
    }
    
    
}