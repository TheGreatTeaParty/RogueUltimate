using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public Transform inventoryParent;
    public Transform quickSlotsParent;
    public ItemSlot[] itemSlots;
    public QuickSlot[] quickSlots;
    public List<Item> items;
 
    public int Gold { get; set; } = 100;

    
    public event Action<ItemSlot> OnClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    public event Action<QuickSlot> OnQuickDropEvent;
    
    
    private void Awake()
    {
        itemSlots = inventoryParent.GetComponentsInChildren<ItemSlot>();
        quickSlots = quickSlotsParent.GetComponentsInChildren<QuickSlot>();
    }
    
    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnClickEvent += OnClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }

        for (int i = 0; i < quickSlots.Length; i++)
        {
            quickSlots[i].OnClickEvent += OnClickEvent;
            quickSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            quickSlots[i].OnDragEvent += OnDragEvent;
            quickSlots[i].OnEndDragEvent += OnEndDragEvent;
            quickSlots[i].OnQuickDropEvent += OnQuickDropEvent;
        }

        SetInventoryOnStart();
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null || (itemSlots[i].Item.ID == item.ID && itemSlots[i].Amount < item.StackMaxSize))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }
        
        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;
                if (itemSlots[i].Amount == 0)
                    itemSlots[i].Item = null;
                
                return true;
            }
        }
        
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
                return false;
        }
        
        return true;
    }

    public void SetInventoryOnStart()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i].GetCopy();
            itemSlots[i].Amount = 1;
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }

        for (int j = 0; j < quickSlots.Length; j++)
        {
            quickSlots[j].Item = null;
            quickSlots[j].Amount = 0;
        }
    }

}