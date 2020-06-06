using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStat:CharacterStat
{
    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }

    
    //Receive message of changing equipment, so change player modifiers
    void OnEquipmentChanged(EquipmentItem newEquipmentItem, EquipmentItem oldEquipmentItem)
    {
        if(newEquipmentItem!= null)
        {
            ph_armor.AddModifier(newEquipmentItem.PhysicalArmorModifier);
            mg_armor.AddModifier(newEquipmentItem.MagicalArmorModifier);

            ph_damage.AddModifier(newEquipmentItem.PhysiscalDamageModifier);
            mg_damage.AddModifier(newEquipmentItem.MagicalDamageModifier);
        }

        if (oldEquipmentItem != null)
        {
            ph_armor.RemoveModifier(oldEquipmentItem.PhysicalArmorModifier);
            mg_armor.RemoveModifier(oldEquipmentItem.MagicalArmorModifier);

            ph_damage.RemoveModifier(oldEquipmentItem.PhysiscalDamageModifier);
            mg_damage.RemoveModifier(oldEquipmentItem.MagicalDamageModifier);
        }
    }
    
    
}
