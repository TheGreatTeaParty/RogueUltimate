using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


[CreateAssetMenu(menuName = "Items/Item")]  
public class Item : ScriptableObject
{
    [SerializeField] protected String name;
    [SerializeField] protected Sprite sprite;
    [Space]
    [SerializeField] protected int stackSize;
    [SerializeField] protected int price;

    public String Name => name;
    public Sprite Sprite => sprite;
    
    
    public virtual void Use()
    {
        Debug.Log("Item has been used");
    }
    
    
    public virtual void Drop()
    {
        InventoryManager.Instance.RemoveItemFromInventory(this);
        InventoryManager.Instance.DropFromInventory(this);
    }


    public virtual void MoveToQuickAccess()
    {
        
    }
    
    
}

