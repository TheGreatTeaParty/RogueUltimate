using System;
using TMPro;
using UnityEngine;


public class TradeUI : MonoBehaviour
{
    private TradeSlot[] playerSlots;
    private TradeSlot[] npcSlots;

    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI relation;
    [Space]
    [SerializeField] private TradeManager tradeManager;
    [SerializeField] private Transform playerSlotsParent;
    [SerializeField] private Transform npcSlotsParent;


    private void Start()
    {
        tradeManager = TradeManager.Instance;
        if (playerSlotsParent == null || npcSlotsParent == null) 
            Debug.Log("Null pointer in TradeUI");
        
        npcSlots = GetComponentsInChildren<TradeSlot>(npcSlotsParent);
        playerSlots = GetComponentsInChildren<TradeSlot>(playerSlotsParent);
        
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
        for (; j < tradeManager.playerInventory.items.Count; j++)
            playerSlots[j].AddItemToTradeSlot(tradeManager.playerInventory.items[j]);

        for (; j < playerSlots.Length; j++)
            playerSlots[j].RemoveItemFromTradeSlot();


        gold.SetText(tradeManager.gold.ToString());       
        relation.SetText(tradeManager.relation.ToString());
    }
    
} 