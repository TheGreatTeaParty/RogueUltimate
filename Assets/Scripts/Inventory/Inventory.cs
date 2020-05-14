using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    public event Action<Item> onItemTouchedEvent;

    
    public void Start()
    {
        foreach (var itemSlot in itemSlots)
            itemSlot.onTouchEvent += onItemTouchedEvent;
        
        UpdateUi();
    }

    
    public void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        
        UpdateUi();
    }

    
    private void UpdateUi()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
            itemSlots[i].Item = items[i];
        
        for (; i < itemSlots.Length; i++)
            itemSlots[i].Item = null;
    }

    public bool CheckFullness()
    {
        return items.Count >= itemSlots.Length;
    }

    
    public bool AddItem(Item item)
    {
        if (CheckFullness())
            return false;
            
        items.Add(item);
        UpdateUi();
        return true;
    }

    
    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            UpdateUi();
            return true;
        }

        return false;
    }
    
}
