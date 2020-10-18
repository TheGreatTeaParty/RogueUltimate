using UnityEngine;

public class DwarfSeller : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        // Bind the info for TradeManager
        var playerInventory = CharacterManager.Instance.Inventory;
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
