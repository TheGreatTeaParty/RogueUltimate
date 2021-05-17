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
}
