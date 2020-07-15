using System;
using UnityEngine;

public class TradeUI : MonoBehaviour
{
    [SerializeField] private TradeSlot[] slotsPl;
    [SerializeField] private TradeSlot[] slotsNpc;
    [SerializeField] private InventoryManager inventoryPl;
    [SerializeField] private InventoryNPC inventoryNpc;


    private void Start()
    {
        inventoryPl = InventoryManager.Instance;
    }

    private void Update()
    {
        for (int i = 0; i < slotsPl.Length; i++)
        {
            
        }
        
        for (int i = 0; i < slotsNpc.Length; i++)
        {
            
        }
    }
} 