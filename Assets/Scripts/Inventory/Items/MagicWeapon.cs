using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MagicWeapon")]

public class MagicWeapon : EquipmentItem
{
    [Space]
    public float castTime;
    public float KnockBack;

    [Space]
    public Transform Prefab;

    private void Awake()
    {
        equipmentType = EquipmentType.weapon;
    }

    public override void Attack(int ph_dmg,int mg_dmg)
    {
        Instantiate(Prefab, KeepOnScene.instance.GetComponent<PlayerMovment>().transform.position, Quaternion.identity);
        //Send mesage to Attack animation handler that we use mele weapon
        KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.magic);
    }
    public override WeaponType Echo()
    {
        return WeaponType.magic;
    }

    public override float GetAttackCD()
    {
        return castTime;
    }
}
