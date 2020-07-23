using System;
using UnityEngine;

// Need setters and getters (?)
public class TradeManager : MonoBehaviour
{
    #region Singleton

    public static TradeManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion

    private bool _state;
    public Item currentItem;
    [Space]
    public InventoryManager playerInventory;
    public NPCInventory npcInventory;
    public TradeTooltip tradeTooltip;
    [Space]
    public int gold;
    public int relation;
    public bool State => _state;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;


    // true - buy, false - sell
    public void SetState(bool state)
    {
        _state = state;
    }
    
    public void OpenTooltip()
    {
        if (currentItem == null)
        {
            Debug.Log("Null pointer in TradeManager.cs, ShowInfo()");
            return;
        }
        
        tradeTooltip.SetName(currentItem.Name);
        tradeTooltip.SetSprite(currentItem.Sprite);
        tradeTooltip.SetDescription(currentItem.Description);
        tradeTooltip.SetPrice(currentItem.Price);
    }

    public void CloseTooltip()
    {
        tradeTooltip.SetName("");
        tradeTooltip.SetDescription("");
        tradeTooltip.SetSprite(null);
        tradeTooltip.SetPrice(-1);
    }
    
    public void Buy()
    {
        if (currentItem == null)
        {
            Debug.Log("Null pointer in TradeManager.cs, Buy()");
            return;
        }

        if (playerInventory.items.Count < playerInventory.size && gold >= currentItem.Price)
        {
            gold -= currentItem.Price;
            playerInventory.AddItemToInventory(currentItem);
        }
        
        onChangeCallback?.Invoke();
    }

    public void Sell()
    {
        if (currentItem == null)
        {
            Debug.Log("Null pointer in TradeManager.cs, Sell()");
            return;
        }
        
        gold += currentItem.Price;
        playerInventory.RemoveItemFromInventory(currentItem);
        
        CloseTooltip();
        currentItem = null;
        
        onChangeCallback?.Invoke();
    }

    public void Bind(InventoryManager playerInventory, NPCInventory npcInventory, TradeTooltip tradeTooltip)
    {
        if (playerInventory == null || npcInventory == null || tradeTooltip == null)
        {
            Debug.Log("Smth didn't found in TradeManager.cs, Bind()");
            return;
        }
        
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;
        this.tradeTooltip = tradeTooltip;
        
        onChangeCallback?.Invoke();
    }
    
} 