using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Purchasing;

public class ItemsDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemsDatabase Instance;
    public delegate void SkinUnlocked();
    public SkinUnlocked OnSkinUnlocked;

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
        UnlockSkin(0);
        UnlockSkin(1);
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
    [Space]
    [SerializeField]
    private Skins[] skins;
    private const string baseId = "com.thegreatteaparty.roguestales.skin.";
    [SerializeField]
    private PurchaseManager purchaseManager;


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
    public void UpdateSkinInfo()
    {
        for (int i = 0; i < skins.Length; ++i)
        {
            if (PlayerPrefs.HasKey(skins[i].ID.ToString()))
            {
                skins[i].Locked = false;
            }
            else
            {
                skins[i].Locked = true;
            }
        }
    }
    public int GetSkinLength()
    {
        return skins.Length;
    }

    public bool IsSkinLocked(int index)
    {
        return skins[index].Locked;
    }

    public void UnlockSkin(int index)
    {
        skins[index].Locked = false;
        PlayerPrefs.SetInt(skins[index].ID.ToString(), 1);
    }
    public void UnlockSkinByID(int ID)
    {
        for (int i = 0; i < skins.Length; ++i)
        {
            if (skins[i].ID == ID)
            {
                UnlockSkin(i);
                break;
            }

        }
    }

    public string GetSkinPurchaseID(int index)
    {
        return baseId + skins[index].ID;
    }
    public void BuyAvatarByIndex(int index)
    {
        purchaseManager.BuyProduct(baseId + skins[index].ID.ToString());
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] skin;
    public int ID;
    public bool Locked;
}