using System;
using TMPro;
using UnityEngine;


public class TradeUI : MonoBehaviour
{
    public TradeSlot[] playerSlots;
    public TradeSlot[] npcSlots;

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
        
        for (int i = 0; i < playerSlots.Length; i++)
            playerSlots[i].OnClick += tradeManager.OnSlotClick;

        for (int i = 0; i < npcSlots.Length; i++)
            npcSlots[i].OnClick += tradeManager.OnSlotClick;

        tradeManager.onChangeCallback += UpdateUI;
    }

    private void UpdateUI()
    {
        var i = 0;
        for (; i < tradeManager.npcInventory.items.Count; i++)
            npcSlots[i].Item = tradeManager.npcInventory.items[i];
        for (; i < npcSlots.Length; i++)
            npcSlots[i].Item = null;
        
        i = 0;
        for (; i < tradeManager.playerInventory.items.Count && i < playerSlots.Length; i++)
        {
            playerSlots[i].Item = tradeManager.playerInventory.itemSlots[i].Item;
            playerSlots[i].Amount = tradeManager.playerInventory.itemSlots[i].Amount;
        }
        for (; i < playerSlots.Length; i++)
        {
            playerSlots[i].Item = null;
            playerSlots[i].Amount = 0;
        }
        
        gold.SetText(tradeManager.playerInventory.Gold.ToString());
        relation.SetText(tradeManager.npcInventory.GetRelation().ToString());
        
        if (tradeManager.currentSlot == null) return;
        action.SetText(tradeManager.currentSlot.tradeSlotType == TradeSlotType.NPC ? "Buy" : "Sell");
        price.SetText(tradeManager.currentSlot.Item.Price.ToString());
    }

} 