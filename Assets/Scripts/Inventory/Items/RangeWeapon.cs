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
        equipmentType = EquipmentType.weapon;
    }

    public override void Attack(int damage)
    {
        Instantiate(arrowPrefab,KeepOnScene.instance.GetComponent<PlayerMovment>().transform.position,Quaternion.identity);

        //Send mesage to Attack animation handler that we use mele weapon
        KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.range);
    }
    
    
    public override WeaponType Echo()
    {
        return WeaponType.range;
    }

    public override float GetAttackCD()
    {
        return attackCD;
    }

}
