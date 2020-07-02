using System.Collections.Generic;
using System.Linq;
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

    private int _count = 0;
    [SerializeField] private List<UsableItem> items;
    [SerializeField] private QuickSlot[] slots;


    private void Start()
    {
        // UpdateUI();
    }


    public void AddItemToQuickAccessSlot(UsableItem usableItem)
    {
        foreach (var item in items)
            if (item == usableItem)
                return;

        _count++;
        if (_count > 3)
        {
            items.RemoveAt(0);
            items.Add(usableItem);
            _count--;
        }
        else items.Add(usableItem);

        UpdateUI();
    }


    public void UseItem(UsableItem usableItem)
    {
        if (usableItem != null)
        {
            usableItem.Use();
            items.Remove(usableItem);
            _count--;
            
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


    public void Request(UsableItem usableItem)
    {
        foreach (var item in items.ToList()) // ToList() prevents InvalidOperationException
        {
            if (usableItem == item)
            {
                items.Remove(usableItem);
                _count--;
                UpdateUI();
            }
        }
        
    }
    
}