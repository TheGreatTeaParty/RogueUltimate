using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StrengthAttribute 
{
    private Stat baseValue;

    public Stat MaxHealth;
    public Stat HPRegeneration;
    public Stat PoisonEffectResistance;
    public Stat CritDamage;

    public StrengthAttribute()
    {
        baseValue = new Stat();
        MaxHealth = new Stat();
        HPRegeneration = new Stat();
        PoisonEffectResistance = new Stat();
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

    private void RecalculateStats()
    {
        //Set Stats base values accordingly
        MaxHealth.SETBASE(8 + baseValue.Value + 6 * baseValue.Value);
        HPRegeneration.SETBASE(0.5f + baseValue.Value * 0.1f);
        PoisonEffectResistance.SETBASE(baseValue.Value * 0.02f);
        CritDamage.SETBASE(0.40f + baseValue.Value * 0.05f);
    }
}
