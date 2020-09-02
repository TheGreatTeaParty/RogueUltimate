using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Item")]  
public class Item : ScriptableObject
{
    [SerializeField] public string ID;
    [SerializeField] protected int price;
    [Space]
    [SerializeField] protected String name;
    [SerializeField] protected String description;
    [SerializeField] protected Sprite sprite;
    [Space]
    [SerializeField] protected int stackSize;
    [SerializeField] protected int amount;
    
    public int Price => price;
    public String Name => name;
    public Sprite Sprite => sprite;
    public string Description => description;
    public int Amount
    {
        get => amount;
        set => amount = value;
    }


    public virtual void Use()
    {
        
    }
    
    
    public void Drop(string place)
    {
        if (place == "Inventory")
        {
            InventoryManager.Instance.RemoveItemFromInventory(this);
            InventoryManager.Instance.DropFromInventory(this);
            AudioManager.Instance.Play("DropItem");
            return;
        }

        if (place == "Equipment")
        {
            EquipmentManager.Instance.DropFromEquipment((EquipmentItem)this);
            AudioManager.Instance.Play("DropItem");
            return;
        }
        
        Debug.Log("There is no place selected");
    }


    public virtual void MoveToQuickAccess()
    {
        
    }
    
    
}

