using UnityEngine;


public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Ring,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public EquipmentType equipmentType;

    public int HealthBonus;
    public int EnduranceBonus;
    public int ManaBonus;
    [Space]
    public int StrengthBonus;
    public int DexterityBonus;
    public int IntelligenceBonus;
}
