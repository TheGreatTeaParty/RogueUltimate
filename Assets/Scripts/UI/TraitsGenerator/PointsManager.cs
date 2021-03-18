using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public int PointsAvailable = 6;
    public StrengthAttribute Strength;
    public AgilityAttribute Agility;
    public IntelligenceAttribute Intelligence;
    [Space]
    public TextMeshProUGUI StrengthPoint;
    public TextMeshProUGUI AgilityPoint;
    public TextMeshProUGUI IntelligencePoint;
    [Space]
    public TextMeshProUGUI MaxMP;
    public TextMeshProUGUI MPRegen;
    public TextMeshProUGUI ElementalResist;
    public TextMeshProUGUI MagicalCrit;
    [Space]
    public TextMeshProUGUI MaxHP;
    public TextMeshProUGUI PhysicalCrit;
    public TextMeshProUGUI PhResistance;
    [Space]
    public TextMeshProUGUI MaxSP;
    public TextMeshProUGUI SPRegen;
    public TextMeshProUGUI CritChance;
    public TextMeshProUGUI Evade;

    public void IncrementStrength()
    {
        Strength.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
        PointsAvailable--;
    }

    public void IncrementAgility()
    {
        Agility.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
        PointsAvailable--;
    }

    public void IncrementIntelligence()
    {
        Intelligence.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
        PointsAvailable--;
    }

    public void DecStrength()
    {
        Strength.ModifyAttribute(new StatModifier(-1, StatModifierType.Flat));
        PointsAvailable++;
    }

    public void DecAgility()
    {
        Agility.ModifyAttribute(new StatModifier(-1, StatModifierType.Flat));
        PointsAvailable++;
    }

    public void DecIntelligence()
    {
        Intelligence.ModifyAttribute(new StatModifier(-1, StatModifierType.Flat));
        PointsAvailable++;
    }

    public void UpdateStrength()
    {
        MaxHP.text = Strength.MaxHealth.Value.ToString();
        PhysicalCrit.text = Strength.CritDamage.Value.ToString();
        PhResistance.text = Strength.BleedResistance.Value.ToString();
    }

    public void UpdateAgility()
    {
        MaxSP.text = Agility.MaxStamina.Value.ToString();
        SPRegen.text = Agility.StaminaRegeniration.Value.ToString();
        CritChance.text = Agility.CritChance.Value.ToString();
        Evade.text = Agility.dodgeChance.Value.ToString();
    }

    public void UpdateInt()
    {
        MaxMP.text = Intelligence.MaxMana.Value.ToString();
        MPRegen.text = Intelligence.ManaRegeniration.Value.ToString();
        ElementalResist.text = Intelligence.FreezeResistance.Value.ToString();
        MagicalCrit.text = Intelligence.MagicalCrit.Value.ToString();
    }

}
