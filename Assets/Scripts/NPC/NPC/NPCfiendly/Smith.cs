using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smith : AI, IInteractable
{
    [Space]
    public List<Item> List2;
    public List<Item> List3;

    private TavernKeeperUpgrade keeperUpgrade;
    private NPCInventory npcInventory;

    public string GetActionName()
    {
        return "Trade";
    }

    public void Interact()
    {
        // Bind the info for TradeManager
        var playerInventory = CharacterManager.Instance.Inventory;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerInventory, npcInventory);
        tradeManager.Open(TradeManager.tradeType.smith);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        state = NPCstate.IDLE;
        TradeManager.Instance.OnTradeUpgraded += Upgrade;
        npcInventory = GetComponent<NPCInventory>();
        keeperUpgrade = TavernKeeperUpgrade.Instance;

        SetInvenotyOnStart();
    }

    public void Upgrade(TradeManager.tradeType type)
    {
        if (type == TradeManager.tradeType.smith)
        {
            if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.smith) == 2)
            {
                npcInventory.items = List2;
                Interact();
            }
            else
            {
                npcInventory.items = List3;
                Interact();
            }
        }
    }

    public void SetInvenotyOnStart()
    {
        if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.smith) == 2)
            npcInventory.items = List2;
        else if(keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.smith) == 3)
            npcInventory.items = List3;
    }
}
