using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentType { weapon, armor, ring, amulet }


[CreateAssetMenu(menuName = "Items/EquipmentItem")]

public class EquipmentItem : Item
{
    public EquipmentType equipmentType;

    //Modifiers
    public int armorModifier;
    public int damageModifier;


    public override void Use()
    {
        EquipmentManager.instance.Equip(this);
        Inventory.instance.Remove(this);
    }
    public virtual void Attack(int damage)
    {
        //It is made to be called in the child class -> weapon
    }
    
}
