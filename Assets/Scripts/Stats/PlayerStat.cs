using System;
using System.Collections.Generic;


public class PlayerStat
{
    public float baseValue;
    public float value {
        get { return CalculateFinalValue(); }
    }
    
    private readonly List<StatModifier> statModifiers;


    public PlayerStat(float baseValue)
    {
        this.baseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }

    
    public void AddModifier(StatModifier mod)
    {
        statModifiers.Add(mod);
    }
    
    
    public bool RemoveModifier(StatModifier mod)
    {
        return statModifiers.Remove(mod);
    }
    
    
    private int CalculateFinalValue()
    {
        float flatMod = 0;
        float percentMod = 0;
        
        foreach (var statModifier in statModifiers)
        {
            if (statModifier.Type == StatModifierType.Flat)
                flatMod += statModifier.Value;
            else if (statModifier.Type == StatModifierType.Percent)
                percentMod += statModifier.Value;
        }

        float finalValue = (baseValue + flatMod) * (1 + percentMod);

        return (int) Math.Round(finalValue, 0);
    }
    
}
