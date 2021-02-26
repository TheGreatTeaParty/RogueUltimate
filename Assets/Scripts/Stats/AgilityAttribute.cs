﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgilityAttribute
{
    private Stat baseValue;

    public Stat MaxStamina;
    public Stat StaminaRegeniration;
    public Stat СonsternationResistance;
    public Stat CritChance;
    public Stat dodgeChance;


    public AgilityAttribute()
    {
        baseValue = new Stat();
        MaxStamina = new Stat();
        StaminaRegeniration = new Stat();
        СonsternationResistance = new Stat();
        CritChance = new Stat();
        dodgeChance = new Stat();
    }
   
    public void ModifyAttribute(StatModifier statModifier)
    {
        baseValue.AddModifier(statModifier);
        RecalculateStats();
    }

    public void RemoveAttribute(object source)
    {
        baseValue.RemoveAllModifiersFromSource(source);
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        MaxStamina.SETBASE(10 + baseValue.Value + baseValue.Value * 5);
        StaminaRegeniration.SETBASE(1 + baseValue.Value * 0.6f);
        СonsternationResistance.SETBASE(baseValue.Value * 0.02f);
        CritChance.SETBASE(baseValue.Value + baseValue.Value * 2.5f);
        dodgeChance.SETBASE(baseValue.Value + baseValue.Value * 2.5f);
    }
}