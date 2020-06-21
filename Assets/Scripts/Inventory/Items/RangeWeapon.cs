using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/RangeWeapon")]

public class RangeWeapon : EquipmentItem
{
    [Space]
    public float attackCD;
    public float KnockBack;

    [Space]
    public Transform arrowPrefab;

    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }

    public override void Attack(int ph_dmg, int mg_dmg)
    {
        Transform arrow = Instantiate(arrowPrefab,KeepOnScene.instance.GetComponent<PlayerMovment>().transform.position,Quaternion.identity);
        arrow.GetComponent<FlyingObject>().SetData(ph_dmg, mg_dmg, InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection());
        //Send mesage to Attack animation handler that we use Melee Weapon
        KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.Range);
    }
    
    
    public override WeaponType Echo()
    {
        return WeaponType.Range;
    }

    public override float GetAttackCD()
    {
        return attackCD;
    }

}
