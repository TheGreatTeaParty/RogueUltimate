using UnityEngine;

public class DwarfSeller : Citizen, IInteractable
{
    [SerializeField] NPCInventory _NPCInventory;
    bool _isGenerated = false;

    public void Interact()
    {
        if (!_isGenerated)
        {
            _NPCInventory = GetComponent<NPCInventory>();
            for (int i = 0; i < 5; ++i)
            {
                _NPCInventory.AddItem(ItemsAsset.instance.GenerateItemBasedLevel(CharacterManager.Instance.Stats.Level + 1));
            }
            for (int i = 0; i < 2; ++i)
            {
                _NPCInventory.AddItem(ItemsAsset.instance.GenerateItemBasedLevel(CharacterManager.Instance.Stats.Level + 2));
            }
            _NPCInventory.AddItem(ItemsAsset.instance.GenerateItemBasedLevel(CharacterManager.Instance.Stats.Level + 3));
            _isGenerated = true;
        }
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
