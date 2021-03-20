using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StrengthAttribute 
{
    private readonly Stat baseValue;

    public Stat MaxHealth;
    public Stat DazeResistance;
    public Stat PoisonResistance;
    public Stat BleedResistance;
    public Stat CritDamage;

    public StrengthAttribute()
    {
        baseValue = new Stat();
        MaxHealth = new Stat();
        DazeResistance = new Stat();
        PoisonResistance = new Stat();
        BleedResistance = new Stat();
        CritDamage = new Stat();
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
    public void RemoveAttribute(StatModifier statModifier)
    {
        baseValue.RemoveModifier(statModifier);
        RecalculateStats();
    }
    public bool RemoveLast()
    {
        var result = baseValue.RemoveLast();
        RecalculateStats();
        return result;
    }

    public void MakeAbsolute()
    {
        baseValue.SETBASE(0);
        RecalculateStats();
    }
    public void ClearAttribute()
    {
        baseValue.RemoveAllModifiers();
    }

    private void RecalculateStats()
    {
        //Set Stats base values accordingly
        MaxHealth.SETBASE(8 + baseValue.Value + 6 * baseValue.Value);
        DazeResistance.SETBASE(baseValue.Value * 0.02f);
        PoisonResistance.SETBASE(baseValue.Value * 0.02f);
        BleedResistance.SETBASE(baseValue.Value * 0.02f);
        CritDamage.SETBASE(0.5f + baseValue.Value * 0.05f);
    }

    public float GetBaseValue()
    {
        return baseValue.Value;
    }

}
