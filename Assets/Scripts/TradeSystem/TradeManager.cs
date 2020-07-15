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


    private void Start()
    {
        inventoryPl = InventoryManager.Instance;
    }

    private void Buy(Item item)
    {
        if (item == null) return;
        
        if (inventoryPl.CheckOverflow() is false)
            inventoryPl.BuyItem(item);
    }

    private void Sell(Item item)
    {
        if (item != null && inventoryNpc.CheckOverflow() is false)
            inventoryPl.SellItem(item, inventoryNpc);
    }
    
    public void SetNpcInventory(InventoryNPC inventory)
    {
        if (inventory == null) return;
        inventoryNpc = inventory;
    }

} 