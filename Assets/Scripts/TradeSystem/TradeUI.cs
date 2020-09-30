using System;
using TMPro;
using UnityEngine;


public class TradeUI : MonoBehaviour
{
    private TradeSlot[] playerSlots;
    private TradeSlot[] npcSlots;

    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI relation;
    [SerializeField] private TextMeshProUGUI action;
    [SerializeField] private TextMeshProUGUI price;
    [Space]
    [SerializeField] private TradeManager tradeManager;
    [SerializeField] private Transform playerSlotsParent;
    [SerializeField] private Transform npcSlotsParent;


    private void Start()
    {
        tradeManager = TradeManager.Instance;
        if (playerSlotsParent == null || npcSlotsParent == null) 
            Debug.Log("Null pointer in TradeUI");
        
        npcSlots = npcSlotsParent.GetComponentsInChildren<TradeSlot>();
        playerSlots = playerSlotsParent.GetComponentsInChildren<TradeSlot>();
        
        tradeManager.onChangeCallback += UpdateUI;
    }

    private void UpdateUI()
    {
        if (tradeManager == null)
        {
            Debug.Log("NullPointer in UpdateUI");
            return;
        }
        
        var i = 0;
        for (; i < tradeManager.npcInventory.items.Count; i++)
            npcSlots[i].AddItemToTradeSlot(tradeManager.npcInventory.items[i]);

        for (; i < npcSlots.Length; i++)
            npcSlots[i].RemoveItemFromTradeSlot();

        
        var j = 0;
       // for (; j < tradeManager.playerCharacter.items.Count; j++)
       //     playerSlots[j].AddItemToTradeSlot(tradeManager.playerCharacter.items[j]);

        for (; j < playerSlots.Length; j++)
            playerSlots[j].RemoveItemFromTradeSlot();


        gold.SetText(tradeManager.playerCharacter.GetGold().ToString());
        relation.SetText(tradeManager.npcInventory.GetRelation().ToString());
        action.SetText(tradeManager.state is true ? "Buy" : "Sell");
        if (tradeManager.currentItem != null) price.SetText(tradeManager.currentItem.Price.ToString());
        
    }
    
} 