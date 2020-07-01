using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Item")]  
public class Item : ScriptableObject
{
    [SerializeField] protected string id;
    public string ID => id;
    
    [Space]
    [SerializeField] protected String name;
    [SerializeField] protected Sprite sprite;
    
    [Space]
    [SerializeField] protected int stackSize;
    [SerializeField] protected int amount;
    [SerializeField] protected int price;

    public String Name => name;
    public Sprite Sprite => sprite;
    public int Amount
    {
        get => amount;
        set => amount = value;
    }


    public virtual void Use()
    {
        
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

