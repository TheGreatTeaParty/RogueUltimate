using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Items/Item")]  
public class Item : ScriptableObject
{
    public String itemName;
    public Sprite itemIcon;
    public Sprite itemSprite;
    [Space]
    public int stackSize;
    public int price;

    
    public Sprite GetSprite()
    {
        return itemSprite;
    }

    
    public virtual void Use()
    {
        Debug.Log("Item has been used");
    }
    
    
    public virtual void Drop()
    {
        InventoryManager.Instance.RemoveItemFromInventory(this);
        InventoryManager.Instance.DropFromInventory(this);
    }
    
    
}

