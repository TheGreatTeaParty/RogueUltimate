using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStat:CharacterStat
{
    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    
    //Receive message of changing equipment, so change player modifiers
    void OnEquipmentChanged(EquipmentItem newEquipmentItem, EquipmentItem oldEquipmentItem)
    {
        if(newEquipmentItem!= null)
        {
            armor.AddModifier(newEquipmentItem.armorModifier);
            damage.AddModifier(newEquipmentItem.damageModifier);
        }

        if (oldEquipmentItem != null)
        {
            armor.RemoveModifier(oldEquipmentItem.armorModifier);
            damage.RemoveModifier(oldEquipmentItem.damageModifier);
        }
    }
    
    
}
