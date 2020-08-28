using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfSeller : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        // Bind the info for TradeManager
        var playerInventory = InventoryManager.Instance;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerInventory, npcInventory);
        tradeManager.Open();
    }

    public string GetActionName()
    {
        return "Trade";
    }
}
