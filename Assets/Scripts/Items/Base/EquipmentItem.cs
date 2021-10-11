using UnityEngine;

public enum EquipmentType
{
    Weapon = 0, 
    Armor = 1, 
    Ring = 2,
    Amulet = 3,
    Rune = 4,
    Attribute = 5,
}
public enum EAttackAnimationType
{
    none = 0,
    Range,
    MeleOneHand,
    MeleTwoHand,
    MeleDagger,

}


[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItem : Item
{
    public ArmorType EqipmnetArmorType = ArmorType.None;
    [SerializeField] protected EquipmentType equipmentType;
    public EAttackAnimationType AttackAnimationType;
    [SerializeField] protected Sprite[] IdleAnimation;
    [Space] 
    [SerializeField] protected int physicalDamageBonus;
    [SerializeField] protected int physicalProtectionBonus;
    [SerializeField] protected int magicDamageBonus;
    [SerializeField] protected int magicProtectionBonus;
    [SerializeField] protected int willBonus;
    [SerializeField] protected int vitalityBonus;
    [SerializeField] protected int mindBonus;
    [SerializeField] protected int agilityBonus;
    [Space]
    [SerializeField] protected float physicalDamagePercentBonus;
    [SerializeField] protected float physicalProtectionPercentBonus;
    [SerializeField] protected float magicDamagePercentBonus;
    [SerializeField] protected float magicProtectionPercentBonus;
    [Space]
    [SerializeField] protected int requiredStrength;
    [SerializeField] protected int requiredIntelligence;
    [SerializeField] protected int requiredAgility;
    [Space]
    [SerializeField] public Effect _effect;
    [Space]
    public Transform AttackEffect;
    public EquipmentType EquipmentType
    {
        get => equipmentType;
        set => equipmentType = value;
    }
    public Sprite[] Animation => IdleAnimation;

    public override void Use()
    {

    }

    public virtual void Equip(PlayerStat stats)
    {
        if (physicalDamageBonus != 0) 
            stats.PhysicalDamage.AddModifier(new StatModifier(physicalDamageBonus, StatModifierType.Flat, this));
        if (physicalProtectionBonus != 0) 
            stats.PhysicalProtection.AddModifier(new StatModifier(physicalProtectionBonus, StatModifierType.Flat, this));
        if (magicDamageBonus != 0) 
            stats.MagicDamage.AddModifier(new StatModifier(magicDamageBonus, StatModifierType.Flat, this));
        if (magicProtectionBonus != 0) 
            stats.MagicProtection.AddModifier(new StatModifier(magicProtectionBonus, StatModifierType.Flat, this));
        if (vitalityBonus != 0)
            stats.Strength.ModifyAttribute(new StatModifier(vitalityBonus, StatModifierType.Flat, this));
        if (mindBonus != 0)
            stats.Intelligence.ModifyAttribute(new StatModifier(mindBonus, StatModifierType.Flat, this));
        if (agilityBonus != 0)
            stats.Agility.ModifyAttribute(new StatModifier(agilityBonus, StatModifierType.Flat, this));
        
        if (physicalDamagePercentBonus != 0) 
            stats.PhysicalDamage.AddModifier(new StatModifier(physicalDamagePercentBonus, StatModifierType.PercentMult, this));
        if (physicalProtectionPercentBonus != 0) 
            stats.PhysicalProtection.AddModifier(new StatModifier(physicalProtectionPercentBonus, StatModifierType.PercentMult, this));
        if (magicDamagePercentBonus != 0) 
            stats.PhysicalDamage.AddModifier(new StatModifier(magicDamagePercentBonus, StatModifierType.PercentMult, this));
        if (magicProtectionPercentBonus != 0) 
            stats.PhysicalProtection.AddModifier(new StatModifier(magicProtectionPercentBonus, StatModifierType.PercentMult, this));
        
    }
    
    public virtual void Unequip(PlayerStat stats)
    {
        stats.PhysicalDamage.RemoveAllModifiersFromSource(this);
        stats.PhysicalProtection.RemoveAllModifiersFromSource(this);
        stats.MagicDamage.RemoveAllModifiersFromSource(this);
        stats.MagicProtection.RemoveAllModifiersFromSource(this);
        stats.Strength.RemoveAttribute(this);
        stats.Intelligence.RemoveAttribute(this);
        stats.Agility.RemoveAttribute(this);
    }

    public virtual void Attack(float physicalDamage, float magicDamage)
    {
        //It is made to be called in the child class -> Weapon
    }
    
    public virtual AttackType Echo()
    {
        return AttackType.None;
    }

    public float GetRequiredStrength()
    {
        return requiredStrength;
    }
    
    public float GetRequiredIntelligence()
    {
        return requiredIntelligence;
    }
    
    public float GetRequiredAgility()
    {
        return requiredAgility;
    }

    public float GetDamageBonus()
    {
        return physicalDamageBonus;
    }
    public enum ArmorType
    {
        None = 0,
        Magical = 1,
        Physical = 2,
    };
}
