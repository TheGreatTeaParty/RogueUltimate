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
    public TraitsGenerator generator;

    [Space]
    public TextMeshProUGUI Points;
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


    private bool _disableStrength = false;
    private bool _disableAgility = false;
    private bool _disableInt = false;
    private PlayerStat _player;

    private void Start()
    {
        if (generator)
        {
            Strength.ModifyAttribute(new StatModifier(0, StatModifierType.Flat));
            Agility.ModifyAttribute(new StatModifier(0, StatModifierType.Flat));
            Intelligence.ModifyAttribute(new StatModifier(0, StatModifierType.Flat));
        }
        else
        {
            _player = CharacterManager.Instance.Stats;
            Strength.ModifyAttribute(new StatModifier(_player.Strength.GetBaseValue(), StatModifierType.Flat));
            Agility.ModifyAttribute(new StatModifier(_player.Agility.GetBaseValue(), StatModifierType.Flat));
            Intelligence.ModifyAttribute(new StatModifier(_player.Intelligence.GetBaseValue(), StatModifierType.Flat));
            SetPoints();
            _player.onChangeCallback += SetPoints;
        }

        UpdateUI();
        ChechZero();
    }

    private void SetPoints()
    {
        float sumA = Strength.GetBaseValue() + Intelligence.GetBaseValue() + Agility.GetBaseValue();
        float sumB = _player.Strength.GetBaseValue() +_player.Intelligence.GetBaseValue() + _player.Agility.GetBaseValue();
        if (sumA - sumB <= 0)
            PointsAvailable = _player.StatPoints;
        if(Strength.GetBaseValue() < _player.Strength.GetBaseValue())
            Strength.ModifyAttribute(new StatModifier(_player.Strength.GetBaseValue(), StatModifierType.Flat));
        if (Intelligence.GetBaseValue() < _player.Intelligence.GetBaseValue())
            Intelligence.ModifyAttribute(new StatModifier(_player.Intelligence.GetBaseValue(), StatModifierType.Flat));
        if (Agility.GetBaseValue() < _player.Agility.GetBaseValue())
            Agility.ModifyAttribute(new StatModifier(_player.Agility.GetBaseValue(), StatModifierType.Flat));
        UpdateUI();
    }

    public void IncrementStrength()
    {
        if (PointsAvailable > 0 && !_disableStrength)
        {
            Strength.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
            PointsAvailable--;
            UpdateStrength();
        }
    }

    public void IncrementAgility()
    {
        if (PointsAvailable > 0 && !_disableAgility)
        {
            Agility.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
            PointsAvailable--;
            UpdateAgility();
        }
    }

    public void IncrementIntelligence()
    {
        if (PointsAvailable > 0 && !_disableInt)
        {
            Intelligence.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
            PointsAvailable--;
            UpdateInt();
        }
    }

    public void DecStrength()
    {
        if (generator)
        {
            if (Strength.GetBaseValue() > 0)
            {
                if (Strength.RemoveLast())
                {
                    PointsAvailable++;
                }
            }
        }
        else
        {
            if (Strength.GetBaseValue() > _player.Strength.GetBaseValue())
            {
                if (Strength.RemoveLast())
                {
                    PointsAvailable++;
                }
            }
        }
        UpdateStrength();
    }

    public void DecAgility()
    {
        if (generator)
        {
            if (Agility.GetBaseValue() > 0)
            {
                if (Agility.RemoveLast())
                {
                    PointsAvailable++;
                }
            }
        }
        else
        {
            if (Agility.GetBaseValue() > _player.Agility.GetBaseValue())
            {
                if (Agility.RemoveLast())
                {
                    PointsAvailable++;
                }
            }
        }
        UpdateAgility();
    }

    public void DecIntelligence()
    {
        if (generator)
        {
            if (Intelligence.GetBaseValue() > 0)
            {
                if (Intelligence.RemoveLast())
                {
                    PointsAvailable++;
                }
            }
        }
        else
        {
            if (Intelligence.GetBaseValue() > _player.Intelligence.GetBaseValue())
            {
                if (Intelligence.RemoveLast())
                {
                    PointsAvailable++;
                }
            }
        }
        UpdateInt();
    }

    private void UpdateStrength()
    {
        Points.text = PointsAvailable.ToString();
        StrengthPoint.text = Strength.GetBaseValue().ToString();
        MaxHP.text = Strength.MaxHealth.Value.ToString();
        PhysicalCrit.text = Strength.CritDamage.Value.ToString() + "%";
        PhResistance.text = Strength.BleedResistance.Value.ToString() + "%";
    }

    private void UpdateAgility()
    {
        Points.text = PointsAvailable.ToString();
        AgilityPoint.text = Agility.GetBaseValue().ToString();
        MaxSP.text = Agility.MaxStamina.Value.ToString();
        SPRegen.text = Agility.StaminaRegeniration.Value.ToString();
        CritChance.text = Agility.CritChance.Value.ToString() + "%"; ;
        Evade.text = Agility.dodgeChance.Value.ToString() + "%";
    }

    private void UpdateInt()
    {
        Points.text = PointsAvailable.ToString();
        IntelligencePoint.text = Intelligence.GetBaseValue().ToString();
        MaxMP.text = Intelligence.MaxMana.Value.ToString();
        MPRegen.text = Intelligence.ManaRegeniration.Value.ToString();
        ElementalResist.text = Intelligence.FreezeResistance.Value.ToString() + "%";
        MagicalCrit.text = Intelligence.MagicalCrit.Value.ToString() + "%";
    }

    public void UpdateUI()
    {
        UpdateStrength();
        UpdateAgility();
        UpdateInt();
    }

    public void SaveChanges()
    {
        var player = CharacterManager.Instance.Stats;
        player.AddAttributePoint(StatType.Physique,Strength.GetBaseValue() - player.Strength.GetBaseValue());
        player.AddAttributePoint(StatType.Mind,Intelligence.GetBaseValue() - player.Intelligence.GetBaseValue());
        player.AddAttributePoint(StatType.Reaction,Agility.GetBaseValue() - player.Agility.GetBaseValue());
    }

    private void ChechZero()
    {
        if (generator)
        {
            for(int i = 0; i < generator.OutcomeTraits.Count; ++i)
            {
                if (generator.OutcomeTraits[i].Name == "Dystrophic")
                    _disableStrength = true;
                else if (generator.OutcomeTraits[i].Name == "Idiot")
                    _disableInt = true;
                else if (generator.OutcomeTraits[i].Name == "Retarded")
                    _disableAgility = true;
            }
        }
        else
        {
            var temp = CharacterManager.Instance.Stats.PlayerTraits.Traits;
            for (int i = 0; i < temp.Count; ++i)
            {
                if (temp[i].Name == "Dystrophic")
                    _disableStrength = true;
                else if (temp[i].Name == "Idiot")
                    _disableInt = true;
                else if (temp[i].Name == "Retarded")
                    _disableAgility = true;
            }
        }
    }
}
