using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Equipment")]  
public class Equipment : Item
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
}

public enum EquipmentType { weapon, armor, ring, amulet }