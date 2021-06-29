using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform quickSlotsParent;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI kills;
    [SerializeField] private ItemSlot[] itemSlots;
    [SerializeField] private QuickSlot[] quickSlots;
    [SerializeField] private List<Item> items;
    private PlayerStat playerStat;
 
    public int Gold { get; set; } = 100;
    public List<Item> Items
    {
        get => items;
        set => items = value;
    }
    public ItemSlot[] ItemSlots
    {
        get => itemSlots;
        set => itemSlots = value;
    }
    public QuickSlot[] QuickSlots
    {
        get => quickSlots;
    }
    private int InventorySlots = 12;


    public event Action<ItemSlot> OnClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    public event Action<QuickSlot> OnQuickDropEvent;
    public event Action<float> OnGoldChanged;
    
    
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
        playerStat = CharacterManager.Instance.Stats;

        playerStat.OnKillChanged += UpdateKillCount;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null || (itemSlots[i].Item.ID == item.ID && itemSlots[i].Amount < item.StackMaxSize))
            {
                itemSlots[i].Item = item;
                itemSlots[i].SetTier(itemSlots[i].Item);
                itemSlots[i].Amount++;
                items.Add(item);
                return true;
            }
        }
        
        return false;
    }
    public bool AddQuickSlotItemOnLoad(Item item)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].Item == null || (quickSlots[i].Item.ID == item.ID && quickSlots[i].Amount < item.StackMaxSize))
            {
                quickSlots[i].Item = item;
                quickSlots[i].SetTier(quickSlots[i].Item);
                quickSlots[i].Amount++;
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
                {
                    itemSlots[i].Item = null;
                    itemSlots[i].SetTier(itemSlots[i].Item);
                    items.Remove(item);
                }
                
                return true;
            }
        }
        
        return false;
    }
    public bool RemoveItemCompletly(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Amount = 0;
                itemSlots[i].Item = null;
                itemSlots[i].SetTier(itemSlots[i].Item);
                items.Remove(item);
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(i == InventorySlots) { return true; }
            if (itemSlots[i].Item == null)
                return false;
        }
        
        return true;
    }

    public void SetInventoryOnStart()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length && i < InventorySlots; i++)
        {
            itemSlots[i].Item = items[i].GetCopy();
            itemSlots[i].Amount = 1;
            itemSlots[i].SetTier(itemSlots[i].Item);
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

        UpdateGold();

    }
    public void ChangeInventoryCapasity(int capacity)
    {
        InventorySlots = capacity;
        for (int i = InventorySlots; i < itemSlots.Length; ++i)
        {
            itemSlots[i].gameObject.SetActive(false);
        }
    }

    public int GetInventoryCapacity()
    {
        return InventorySlots;
    }

    public void UpdateGold()
    {
        gold.SetText(Gold.ToString());
        OnGoldChanged?.Invoke(Gold);
    }

    private void UpdateKillCount()
    {
        kills.SetText(playerStat.Kills.ToString());
    }

}