using System;
using UnityEngine;

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
    
    [SerializeField] private InventoryNPC inventoryNpc;
    [SerializeField] private InventoryManager inventoryPl;
    [SerializeField] private TradeTooltip tooltip;
    // !!! NEED A CALL SetTradeInventory() FROM ANY NPC INVENTORY BEFORE WORKING

    private void Start()
    {
        inventoryPl = InventoryManager.Instance;
        tooltip = TradeTooltip.Instance;
    }

    public void Buy(Item item)
    {
        if (item == null) return;
        
        if (inventoryPl.CheckOverflow() is false)
            inventoryPl.BuyItem(item);
    }

    public void Sell(Item item)
    {
        if (item == null || inventoryNpc.CheckOverflow() is true) return;
        
        tooltip.Erase();
        inventoryPl.SellItem(item, inventoryNpc);
    }
    
    public void SetNpcInventory(InventoryNPC inventory)
    {
        if (inventory == null) return;
        inventoryNpc = inventory;
    }

} 