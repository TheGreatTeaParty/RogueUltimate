using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public int size = 8;
    public List<Item> items = new List<Item>();

    //Creating deligate to send update message when something has been changed;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public bool Add(Item item)
    {
        if (items.Count >= size)
        {
            return false;
        }

        //Add item to the list and call update function in InventoryUI class
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }
    public void Remove(Item item)
    {
        //Call spawn function on the player's position
        Vector3 new_pos = new Vector3(KeepOnScene.instance.transform.position.x + 1f, KeepOnScene.instance.transform.position.y, KeepOnScene.instance.transform.position.z);
        Item_Scene.SpawnItemScene(new_pos, item);

        //Remove from the inventory and call update function in InventoryUI class
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
