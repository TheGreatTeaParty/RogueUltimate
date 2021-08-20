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
            if (item)
            {
                if (item.tier == Tier.First)
                {
                    tierOne.Add(item);
                }
                else if (item.tier == Tier.Second)
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
    [SerializeField]
    private Item[] treasures;
    [SerializeField]
    private Contract[] contracts;
    [SerializeField]
    private Ability[] abilities;
    [SerializeField]
    private Skins[] skins;


    public Item GetItemByID(int ID)
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if(allItems[i])
                if (ID == allItems[i].ID) return allItems[i];
        }
        return null;
    }
    public Contract GetContractByID(int ID)
    {
        for (int i = 0; i < contracts.Length; i++)
        {
            if(contracts[i])
                if (ID == contracts[i].ID) return contracts[i];
        }
        return null;
    }
    public Ability GetAbilityByID(int ID)
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i])
                if (ID == abilities[i].ID) return abilities[i];
        }
        return null;
    }
    public Item GetRandomTreasure()
    {
        return treasures[Random.Range(0, treasures.Length - 1)];
    }
    public Sprite[] GetSkinAnimation(int index)
    {
        if(index < skins.Length)
            return skins[index].skin;
        return null;
    }
    public int GetSkinLength()
    {
        return skins.Length;
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] skin;
}
