using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStat : CharacterStat
{
    #region Singleton

    public static PlayerStat Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion
    
    
    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }


    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        
    }


    //Receive message of changing equipment, so change player modifiers
    void OnEquipmentChanged(EquipmentItem newEquipmentItem, EquipmentItem oldEquipmentItem)
    {
        if (newEquipmentItem != null)
        {
            physicalProtection.AddModifier(newEquipmentItem.PhysicalArmorModifier);
            magicProtection.AddModifier(newEquipmentItem.MagicalArmorModifier);

            physicalDamage.AddModifier(newEquipmentItem.PhysiscalDamageModifier);
            magicDamage.AddModifier(newEquipmentItem.MagicalDamageModifier);
        }

        if (oldEquipmentItem != null)
        {
            physicalProtection.RemoveModifier(oldEquipmentItem.PhysicalArmorModifier);
            magicProtection.RemoveModifier(oldEquipmentItem.MagicalArmorModifier);

            physicalDamage.RemoveModifier(oldEquipmentItem.PhysiscalDamageModifier);
            magicDamage.RemoveModifier(oldEquipmentItem.MagicalDamageModifier);
        }
    }
    
    
}
