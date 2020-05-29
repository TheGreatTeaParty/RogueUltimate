using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStat:CharacterStat
{
    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    //Recieve message of changing equipmnet, so change player modifiers
    void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment)
    {
        if(newEquipment!= null)
        {
            armor.AddModifier(newEquipment.armorModifier);
            damage.AddModifier(newEquipment.damageModifier);
        }

        if (oldEquipment != null)
        {
            armor.RemoveModifier(oldEquipment.armorModifier);
            damage.RemoveModifier(oldEquipment.damageModifier);
        }
    }
}
