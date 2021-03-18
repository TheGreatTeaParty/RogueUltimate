using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntelligenceAttribute
{
    private readonly Stat baseValue;

    public Stat MaxMana;
    public Stat ManaRegeniration;
    public Stat ElementalEffectResistance;
    public Stat MagicalCrit;

    public IntelligenceAttribute()
    {
        baseValue = new Stat();
        MaxMana = new Stat();
        ManaRegeniration = new Stat();
        ElementalEffectResistance = new Stat();
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
        ElementalEffectResistance.SETBASE(baseValue.Value * 0.02f);
        MagicalCrit.SETBASE(0.5f + baseValue.Value * .05f);
    }

    public float GetBaseValue()
    {
        return baseValue.Value;
    }
}
