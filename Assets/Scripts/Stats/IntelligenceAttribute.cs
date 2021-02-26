﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntelligenceAttribute
{
    private Stat baseValue;

    public Stat MaxMana;
    public Stat ManaRegeniration;
    public Stat DazeResistance;
    public Stat MagicalCrit;

    public IntelligenceAttribute()
    {
        baseValue = new Stat();
        MaxMana = new Stat();
        ManaRegeniration = new Stat();
        DazeResistance = new Stat();
        MagicalCrit = new Stat();
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
        MaxMana.SETBASE(10 + baseValue.Value + baseValue.Value * 5);
        ManaRegeniration.SETBASE(1 + baseValue.Value * 0.6f);
        DazeResistance.SETBASE(baseValue.Value * 0.02f);
        MagicalCrit.SETBASE(0.40f + baseValue.Value * .05f);
    }
}