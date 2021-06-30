using UnityEngine;

public class ItemsAsset : MonoBehaviour
{
    #region Singleton
    public static ItemsAsset instance;
    void Awake()
    {
        if (instance != null)
            return;
        
        instance = this;
    }
    #endregion

    public Item[] items;
    public Item[] rare_items;
    public Transform Item_prefab;

    public Item GenerateItem()
    {
        return items[Random.Range(0, items.Length)];
    }
    public Item GenerateRareItem()
    {
        return rare_items[Random.Range(0, rare_items.Length)];
    }

    public Item GenerateItemBasedLevel(int level)
    {
        ItemsDatabase items = ItemsDatabase.Instance;
        if (level >= 0 && level < 3)
        {
            return items.tierOne[Random.Range(0, items.tierOne.Count)];
        }
        else if( level >= 3 && level < 6)
        {
            return items.tierTwo[Random.Range(0, items.tierTwo.Count)];
        }
        else if (level >= 6 && level < 9)
        {
            return items.tierThree[Random.Range(0, items.tierThree.Count)];
        }
        else if (level >= 9 && level < 12)
        {
            return items.tierFour[Random.Range(0, items.tierFour.Count)];
        }
        else
        {
            return items.tierFive[Random.Range(0, items.tierFive.Count)];
        }

    }
}
