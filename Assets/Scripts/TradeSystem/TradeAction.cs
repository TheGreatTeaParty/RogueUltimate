using System;
using UnityEngine;

public class TradeAction : MonoBehaviour
{

    public void Bind()
    {
        var playerInventory = InventoryManager.Instance;
        var npcInventory = GetComponent<NPCInventory>();

        if (playerInventory == null || npcInventory == null)
        {
            Debug.Log("Inventory didn't found in TradeAction");
            return;
        }
        
        TradeManager.Instance.Bind(playerInventory, npcInventory);
    }

} 