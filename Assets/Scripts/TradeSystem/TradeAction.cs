using System;
using UnityEngine;

// Change it on Interface
public class TradeAction : MonoBehaviour
{
    public void Bind()
    {
        var playerInventory = InventoryManager.Instance;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeTooltip = GetComponent<TradeTooltip>();
        var tradeManager = TradeManager.Instance;
        
        tradeManager.Bind(playerInventory, npcInventory, tradeTooltip);
        
        tradeManager.gold = playerInventory.GetGold();
        tradeManager.relation = npcInventory.relation;
    }
    
    // true - buy, false - sell
    public void Trade()
    {
        var tradeManager = TradeManager.Instance;
        
        if (tradeManager.State && tradeManager.currentItem != null)
            tradeManager.Buy();
        else
            tradeManager.Sell();
    }

    public void Close()
    {
        var tradeManager = TradeManager.Instance;
        
        tradeManager.CloseTooltip();
        tradeManager.currentItem = null;
    }

} 