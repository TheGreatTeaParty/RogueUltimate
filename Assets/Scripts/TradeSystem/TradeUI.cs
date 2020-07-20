using System;
using UnityEngine;


public class TradeUI : MonoBehaviour
{
    private TradeSlot[] playerSlots;
    private TradeSlot[] npcSlots;

    [SerializeField] private TradeManager tradeManager;
    [SerializeField] private Transform playerSlotsParent;
    [SerializeField] private Transform npcSlotsParent;

    private void Start()
    {
        tradeManager = TradeManager.Instance;
        if (playerSlotsParent == null || npcSlotsParent == null) 
            Debug.Log("Null pointer in TradeUI");

        playerSlots = GetComponentsInChildren<TradeSlot>(playerSlotsParent);
        npcSlots = GetComponentsInChildren<TradeSlot>(npcSlotsParent);
    }

    private void UpdateUI()
    {
        var i = 0;
        for (; i < tradeManager.npcInventory.items.Count; i++)
            npcSlots[i].Image.sprite = tradeManager.npcInventory.items[i].Sprite;

        for (; i < npcSlots.Length; i++)
            npcSlots[i].Image.sprite = null;

        var j = 0;
        for (; j < tradeManager.npcInventory.items.Count; j++)
            playerSlots[j].Image.sprite = tradeManager.playerInventory.items[j].Sprite;

        for (; j < npcSlots.Length; j++)
            playerSlots[j].Image.sprite = null;
    }
    
} 