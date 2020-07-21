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
        
        TradeManager.Instance.Bind(playerInventory, npcInventory, tradeTooltip);
        
        TradeManager.Instance.gold = playerInventory.GetGold();
        TradeManager.Instance.relation = npcInventory.relation;

        TradeManager.Instance.enabled = true;
    }

    public void Close()
    {
        TradeManager.Instance.CloseTooltip();
        TradeManager.Instance.currentItem = null;
        TradeManager.Instance.enabled = false;
    }

} 