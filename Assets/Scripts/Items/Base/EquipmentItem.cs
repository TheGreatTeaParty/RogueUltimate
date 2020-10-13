using UnityEngine;

public enum EquipmentType
{
    Weapon = 0, 
    Armor = 1, 
    Ring = 2,
    Amulet = 3,
    Rune = 4,
    Attribute = 5,
    Lamp = 6
    
}


[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItem : Item
{
    public EquipmentType equipmentType;
    public RuntimeAnimatorController EquipmentAnimations;
    public Sprite[] Animation;
    [Space] 
    public int physicalDamageBonus;
    public int physicalProtectionBonus;
    public int magicDamageBonus;
    public int magicProtectionBonus;
    public int willBonus;
    public int vitalityBonus;
    public int mindBonus;
    public int agilityBonus;
    [Space]
    public float physicalDamagePercentBonus;
    public float physicalProtectionPercentBonus;
    public float magicDamagePercentBonus;
    public float magicProtectionPercentBonus;


    public override void Use()
    {

    }

    public virtual void Equip(PlayerStat stats)
    {
        if (physicalDamageBonus != 0) 
            stats.physicalDamage.AddModifier(new StatModifier(physicalDamageBonus, StatModifierType.Flat, this));
        if (physicalProtectionBonus != 0) 
            stats.physicalProtection.AddModifier(new StatModifier(physicalProtectionBonus, StatModifierType.Flat, this));
        if (magicDamageBonus != 0) 
            stats.magicDamage.AddModifier(new StatModifier(magicDamageBonus, StatModifierType.Flat, this));
        if (magicProtectionBonus != 0) 
            stats.magicProtection.AddModifier(new StatModifier(magicProtectionBonus, StatModifierType.Flat, this));
        if (willBonus != 0)
            stats.will.AddModifier(new StatModifier(willBonus, StatModifierType.Flat, this));
        if (vitalityBonus != 0)
            stats.physique.AddModifier(new StatModifier(vitalityBonus, StatModifierType.Flat, this));
        if (mindBonus != 0)
            stats.mind.AddModifier(new StatModifier(mindBonus, StatModifierType.Flat, this));
        if (agilityBonus != 0)
            stats.reaction.AddModifier(new StatModifier(agilityBonus, StatModifierType.Flat, this));
        
        if (physicalDamagePercentBonus != 0) 
            stats.physicalDamage.AddModifier(new StatModifier(physicalDamagePercentBonus, StatModifierType.PercentMult, this));
        if (physicalProtectionPercentBonus != 0) 
            stats.physicalProtection.AddModifier(new StatModifier(physicalProtectionPercentBonus, StatModifierType.PercentMult, this));
        if (magicDamagePercentBonus != 0) 
            stats.physicalDamage.AddModifier(new StatModifier(magicDamagePercentBonus, StatModifierType.PercentMult, this));
        if (magicProtectionPercentBonus != 0) 
            stats.physicalProtection.AddModifier(new StatModifier(magicProtectionPercentBonus, StatModifierType.PercentMult, this));
        
    }
    
    public virtual void Unequip(PlayerStat stats)
    {
        stats.physicalDamage.RemoveAllModifiersFromSource(this);
        stats.physicalProtection.RemoveAllModifiersFromSource(this);
        stats.magicDamage.RemoveAllModifiersFromSource(this);
        stats.magicProtection.RemoveAllModifiersFromSource(this);
        stats.will.RemoveAllModifiersFromSource(this);
        stats.physique.RemoveAllModifiersFromSource(this);
        stats.mind.RemoveAllModifiersFromSource(this);
        stats.reaction.RemoveAllModifiersFromSource(this);
    }

    public virtual void Attack(float physicalDamage, float magicDamage)
    {
        //It is made to be called in the child class -> Weapon
    }

    public virtual float GetAttackCD()
    {
        return 0;
    }
    
    public virtual AttackType Echo()
    {
        return AttackType.None;
    }
    
}
