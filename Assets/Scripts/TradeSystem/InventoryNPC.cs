using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryNPC : MonoBehaviour
{
    private const int Size = 15;
    [SerializeField] private List<Item> items;


    public void AddItemToInventory(Item item)
    {
        if (item == null) return;
        
        if (items.Count < Size)
            items.Add(item);
    }

    public void RemoveItemFromInventory(Item item)
    {
        if (item == null) return;

        items.Remove(item);
    }
    
    public void SetTradeInventory()
    {
        TradeManager.Instance.SetNpcInventory(this);
    }

    public bool CheckOverflow()
    {
        return items.Count >= Size;
    }
    
} 