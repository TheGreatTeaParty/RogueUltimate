using System;
using UnityEngine;

// Need setters and getters for panels 
public class TradeManager : MonoBehaviour
{
    #region Singleton

    public static TradeManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion

    public TradeNpcPanel npcPanel;
    public TradePlayerPanel playerPanel;

    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;

    private void Start()
    {
        npcPanel = GetComponentInChildren<TradeNpcPanel>();
        playerPanel = GetComponentInChildren<TradePlayerPanel>();
    }

    public void Bind(InventoryManager playerInventory, NPCInventory npcInventory)
    {
        playerPanel.playerInventory = playerInventory;
        npcPanel.npcInventory = npcInventory;
    }
    
} 