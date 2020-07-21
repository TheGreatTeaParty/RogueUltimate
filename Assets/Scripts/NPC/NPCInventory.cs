using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : MonoBehaviour
{
    public int gold;     // optional
    public int relation; // optional
    [Space]
    public List<Item> items;

    
    public int GetRelation()
    {
        return relation;
    }
    
} 