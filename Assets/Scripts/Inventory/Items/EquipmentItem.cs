using UnityEngine;


public enum EquipmentType
{
    Weapon, 
    Armor, 
    Ring, 
    Amulet 
    
}


[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItem : Item
{
    public EquipmentType equipmentType;
    public bool isEquiped;
    
    //Modifiers
    public int PhysicalArmorModifier;
    public int MagicalArmorModifier;

    [Space]
    public int PhysiscalDamageModifier;
    public int MagicalDamageModifier;


    public override void Use()
    {
        currentCount = 1;
        stackSize = 1;
        EquipmentManager.Instance.Equip(this);
        InventoryManager.Instance.RemoveItemFromInventory(this);
    }
    

    public override void Drop()
    {
        if (isEquiped) EquipmentManager.Instance.DropFromEquipment(this);
        else base.Drop(); 
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
