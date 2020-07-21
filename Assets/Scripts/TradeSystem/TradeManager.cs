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

    public Item currentItem;
    [Space]
    public InventoryManager playerInventory;
    public NPCInventory npcInventory;
    public TradeTooltip tradeTooltip;
    [Space]
    public int gold;
    public int relation;
    

    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;



    public void OpenTooltip()
    {
        if (currentItem == null)
        {
            Debug.Log("Null pointer in TradeManager.cs, ShowInfo()");
            return;
        }
        
        tradeTooltip.SetName(currentItem.Name);
        tradeTooltip.SetSprite(currentItem.Sprite);
        tradeTooltip.SetPrice(currentItem.Price);
        // add description 
    }

    public void CloseTooltip()
    {
        tradeTooltip.SetName("");
        tradeTooltip.SetSprite(null);
        tradeTooltip.SetPrice(-1);
        // add description
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