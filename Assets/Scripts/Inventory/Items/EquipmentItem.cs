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
    public bool isEquiped = false;
    
    //Modifiers
    public int PhysicalArmorModifier;
    public int MagicalArmorModifier;

    [Space]
    public int PhysiscalDamageModifier;
    public int MagicalDamageModifier;


    public override void Use()
    {
        EquipmentManager.Instance.Equip(this);
        InventoryManager.Instance.RemoveItemFromInventory(this);
    }


    public override void Drop()
    {
        if (isEquiped) EquipmentManager.Instance.DropFromEquipment(this);
        else base.Drop(); 
    }


    public virtual void Attack(int ph_damage,int mg_damage)
    {
        //It is made to be called in the child class -> weapon
    }

    public virtual float GetAttackCD()
    {
        return 0;
    }


    public virtual WeaponType Echo()
    {
        return WeaponType.nothing;
    }


}
