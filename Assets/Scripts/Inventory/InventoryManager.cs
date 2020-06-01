using System.Collections;
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

    public int size = 8;
    public List<Item> items = new List<Item>();

    //Creating deligate to send update message when something has been changed;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public bool AddItemToInventory(Item item)
    {
        if (items.Count >= size)
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

    
    public void Drop(Item item)
    {
        //Call spawn function on the player's position
        var position = KeepOnScene.instance.transform.position;
        Vector3 newPosition = new Vector3(position.x + 1f, position.y, position.z);
        ItemScene.SpawnItemScene(newPosition, item);
    }
    
    
}
