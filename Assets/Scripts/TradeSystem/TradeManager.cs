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
    
    // State: true - buy, false - sell
    public bool state;
    public Item currentItem;
    [Space]
    public InventoryManager playerInventory;
    public NPCInventory npcInventory;
    public TradeTooltip tradeTooltip;
    public GameObject UI;
    [Space]
    public int gold;
    public int relation;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;
    

    public void DrawTooltip()
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

    public void EraseTooltip()
    {
        tradeTooltip.SetName("");
        tradeTooltip.SetDescription("");
        tradeTooltip.SetSprite(null);
        tradeTooltip.SetPrice(-1);
    }

    public void Trade()
    {
        // State: true - buy, false - sell
        if (state && currentItem != null)
            Buy();
        else
            Sell();
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

        currentItem = null;
        
        onChangeCallback?.Invoke();
    }

    public void Bind(InventoryManager playerInventory, NPCInventory npcInventory)
    {
        if (playerInventory == null || npcInventory == null || tradeTooltip == null)
        {
            Debug.Log("Smth didn't found in TradeManager.cs, Bind()");
            return;
        }
        
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;
        onChangeCallback?.Invoke();
    }

    public void Open()
    {
        UI.SetActive(true);
    }

    public void Close()
    {
        EraseTooltip();
        UI.SetActive(false);
        currentItem = null;
    }
    
} 
