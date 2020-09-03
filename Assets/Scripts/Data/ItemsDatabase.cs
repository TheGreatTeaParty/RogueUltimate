using System;
using System.Diagnostics;
using System.Linq;
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
        return allItems.FirstOrDefault(item => ID == item.ID);
    }
    
}
