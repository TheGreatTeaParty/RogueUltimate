using System;
using UnityEngine;

// Need setters and getters for inventories (?)
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

    public InventoryManager playerInventory;
    public NPCInventory npcInventory;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;
    
    public void Bind(InventoryManager playerInventory, NPCInventory npcInventory)
    {
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;
    }
    
} 