using UnityEngine;

public class DwarfSeller : Citizen,IInteractable
{
    [SerializeField] NPCInventory _NPCInventory;
    public void Interact()
    {
            // Bind the info for TradeManager
            var playerInventory = CharacterManager.Instance.Inventory;
            var tradeManager = TradeManager.Instance;

            tradeManager.Bind(playerInventory, _NPCInventory);
            tradeManager.Open(TradeManager.tradeType.dwarf);
    }

    public string GetActionName()
    {
           return "Trade";
    }
}
