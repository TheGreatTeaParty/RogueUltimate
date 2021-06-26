using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : AI, IInteractable
{
    [Space]
    public List<Item> List2;
    public List<Item> List3;

    private TavernKeeperUpgrade keeperUpgrade;
    private NPCInventory npcInventory;

    public string GetActionName()
    {
        return "Quests";
    }

    public void Interact()
    {
        // Bind the info for TradeManager
        var playerContracts = CharacterManager.Instance.Stats.PlayerContracts;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerContracts, npcInventory);
        tradeManager.Open(TradeManager.tradeType.master);
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
        if (type == TradeManager.tradeType.master)
        {
            if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 2)
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
        if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 2)
            npcInventory.items = List2;
        else if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 3)
            npcInventory.items = List3;
    }

}
