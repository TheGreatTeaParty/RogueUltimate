using UnityEngine;

public enum EquipmentType
{
    Weapon = 0, 
    Armor = 1, 
    Ring = 2, 
    Amulet = 3 
    
}


[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItem : Item
{
    public EquipmentType equipmentType;
    public RuntimeAnimatorController EquipmentAnimations;
    public Sprite[] Animation;
    [Space]
    //Modifiers
    public int PhysicalArmorModifier;
    public int MagicalArmorModifier;

    [Space]
    public int PhysiscalDamageModifier;
    public int MagicalDamageModifier;


    public override void Use()
    {
        EquipmentManager.Instance.Equip(this);
        InventoryManager.Instance.RemoveItemFromInventory(this);
    }


    public virtual void Attack(int ph_damage,int mg_damage)
    {
        //It is made to be called in the child class -> Weapon
    }

    public virtual float GetAttackCD()
    {
        return 0;
    }


    public virtual WeaponType Echo()
    {
        return WeaponType.None;
    }


}
