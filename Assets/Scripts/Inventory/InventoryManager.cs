using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager Instance;
    void Awake()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    }
    #endregion
        
    
    [SerializeField] private int gold = 10;
    [Space]
    public int size = 8;
    public List<Item> items;
    

    //Creating deligate to send update message when something has been changed;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public void Start()
    {
        // Load items on save
        if (SaveManager.LoadPlayer() == null) return;
        
        var data = SaveManager.LoadPlayer();
        gold = data.gold;
        foreach (var id in data.inventoryData)
        {
            if (id != 0) 
                AddItemToInventory(ItemsDatabase.Instance.GetItemByID(id));
        }
        
    }

    public bool AddItemToInventory(Item item)
    {
        if (CheckOverflow() is true)
            return false;

        //AddItemToInventory item to the list and call update function in InventoryUI class
        items.Add(item);
        onItemChangedCallback?.Invoke();

        return true;
    }

    public void RemoveItemFromInventory(Item item)
    {
        //RemoveItemFromInventory from the inventory and call update function in InventoryUI class
        items.Remove(item);
        onItemChangedCallback?.Invoke();
    }
    
    public void DropFromInventory(Item item)
    {
        
        //Call spawn function on the player's position
        var position = KeepOnScene.instance.transform.position;
        Vector3 newPosition = new Vector3(position.x + 1f, position.y, 0f);
        Collider2D checkWall = Physics2D.OverlapCircle(newPosition, 0.25f, LayerMask.GetMask("Wall"));
        if(checkWall == null)
            ItemScene.SpawnItemScene(newPosition, item);
        else
        {
            ItemScene.SpawnItemScene(new Vector3(position.x-1f,position.y,0f), item);
        }
    }

    public bool CheckOverflow()
    {
        return items.Count >= size;
    }

    public void ChangeGold(int value)
    {
        gold += value;
    }

    public void SetGold(int value)
    {
        gold = value;
    }

    public int GetGold()
    {
        return gold;
    }

}
