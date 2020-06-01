using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentType
{
    weapon, 
    armor, 
    ring, 
    amulet 
    
}


[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItem : Item
{
    public EquipmentType equipmentType;
    public bool isEquiped;
    
    //Modifiers
    public int armorModifier;
    public int damageModifier;


    public override void Use()
    {
        EquipmentManager.Instance.Equip(this);
        InventoryManager.Instance.RemoveItemFromInventory(this);
    }


    public override void Drop()
    {
        if (isEquiped) EquipmentManager.Instance.DropFromEquipment(this);
        else base.Drop(); // override
    }


    public virtual void Attack(int damage)
    {
        //It is made to be called in the child class -> weapon
    }


    public virtual WeaponType Echo()
    {
        return WeaponType.nothing;
    }


}
