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
        DontDestroyOnLoad(this);
    }

    #endregion

    public Item[] allItems;
    [Space] 
    public Item[] tierOne;
    public Item[] tierTwo;
    public Item[] tierThree;
    public Item[] tierFour;
    public Item[] tierFive;
    [Space]
    public Item[] treasures;


    public Item GetItemByID(int ID)
    {
        for (int i = 0; i < allItems.Length; i++)
            if (ID == allItems[i].ID) return allItems[i];
        
        return null;
    }
    
}
