using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgilityAttribute
{
    private readonly Stat baseValue;

    public Stat MaxStamina;
    public Stat StaminaRegeniration;
    public Stat CritChance;
    public Stat dodgeChance;


    public AgilityAttribute()
    {
        baseValue = new Stat();
        MaxStamina = new Stat();
        StaminaRegeniration = new Stat();
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
    public bool RemoveLast()
    {
        var result = baseValue.RemoveLast();
        RecalculateStats();
        return result;
    }
    public void MakeAbsolute()
    {
        baseValue.SETBASE(0);
    }
    public void ClearAttribute()
    {
        baseValue.RemoveAllModifiers();
    }
    private void RecalculateStats()
    {
        MaxStamina.SETBASE(16 + baseValue.Value * 3);
        StaminaRegeniration.SETBASE(4 + baseValue.Value * 0.6f);
        CritChance.SETBASE((int)(0.03 * baseValue.Value/(1 + 0.03 * baseValue.Value))*100); //5 + baseValue.Value * 1.72f
        dodgeChance.SETBASE((int)((0.015 * baseValue.Value) / (1 + 0.015* baseValue.Value) * 100));
    }

    public float GetBaseValue()
    {
        return baseValue.Value;
    }
}
