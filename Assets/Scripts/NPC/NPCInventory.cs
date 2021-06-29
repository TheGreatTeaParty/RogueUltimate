using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCInventory : MonoBehaviour
{
    public int gold;     // optional
    public int relation; // optional
    [Space]
    public List<Item> items;
    public Action<Item> onItemAdded;
    
    public int GetRelation()
    {
        return relation;
    }
    public void AddItem(Item item)
    {
        items.Add(item);
        onItemAdded?.Invoke(item);
    }
} 