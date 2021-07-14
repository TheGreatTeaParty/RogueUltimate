using UnityEngine;
using System.Collections.Generic;

public class ItemsDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemsDatabase Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        DontDestroyOnLoad(this);

        tierOne = new List<Item>();
        tierTwo = new List<Item>();
        tierThree = new List<Item>();
        tierFour = new List<Item>();
        tierFive = new List<Item>();

        foreach (var item in allItems)
        {
            if(item.tier == Tier.First)
            {
                tierOne.Add(item);
            }
            else if(item.tier == Tier.Second)
            {
                tierTwo.Add(item);
            }
            else if (item.tier == Tier.Third)
            {
                tierThree.Add(item);
            }
            else if (item.tier == Tier.Fourth)
            {
                tierFour.Add(item);
            }
            else if (item.tier == Tier.Fifth)
            {
                tierFive.Add(item);
            }
        }
    }

    #endregion

    public Item[] allItems;
    [Space]
    [HideInInspector]
    public List<Item> tierOne;
    [HideInInspector]
    public List<Item> tierTwo;
    [HideInInspector]
    public List<Item> tierThree;
    [HideInInspector]
    public List<Item> tierFour;
    [HideInInspector]
    public List<Item> tierFive;
    [Space]
    public Item[] treasures;
    public Contract[] contracts;


    public Item GetItemByID(int ID)
    {
        for (int i = 0; i < allItems.Length; i++)
            if (ID == allItems[i].ID) return allItems[i];
        
        return null;
    }
    public Contract GetContractByID(int ID)
    {
        for (int i = 0; i < contracts.Length; i++)
            if (ID == contracts[i].ID) return contracts[i];

        return null;
    }
}
