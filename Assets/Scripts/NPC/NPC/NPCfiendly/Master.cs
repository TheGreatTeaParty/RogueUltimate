﻿using System.Collections;
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
        npcInventory = GetComponent<NPCInventory>();
        keeperUpgrade = TavernKeeperUpgrade.Instance;
        keeperUpgrade.OnUpgraded += Upgrade;

        SetInvenotyOnStart();
    }

    public void Upgrade(TradeManager.tradeType type)
    {
        if (type == TradeManager.tradeType.master)
        {
            if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 2)
            {
                foreach (var item in List2)
                {
                    npcInventory.AddItem(item);
                }
            }
            else if(keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 3)
            {
                foreach (var item in List3)
                {
                    npcInventory.AddItem(item);
                }
            }
        }
    }

    public void SetInvenotyOnStart()
    {
        if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 2)
        {
            foreach (var item in List2)
            {
                npcInventory.AddItem(item);
            }
        }
        else if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.master) == 3)
        {
            foreach (var item in List2)
            {
                npcInventory.AddItem(item);
            }
            foreach (var item in List3)
            {
                npcInventory.AddItem(item);
            }
        }
    }

}
