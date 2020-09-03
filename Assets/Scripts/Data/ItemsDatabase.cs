using System;
using System.Diagnostics;
using UnityEngine;


public class ItemsDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemsDatabase Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion

    public Item[] allItems;
    [Space] 
    public Item[] tierOne;
    public Item[] tierTwo;
    public Item[] tierThree;
    public Item[] tierFour;
    public Item[] tierFive;


    public Item GetItemByID(int ID)
    {
        foreach (var item in allItems)
            if (ID == item.ID) return item;
        
        return null;
    }
    
}
