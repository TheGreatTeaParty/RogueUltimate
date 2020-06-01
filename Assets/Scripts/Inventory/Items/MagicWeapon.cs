using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MagicWeapon")]

public class MagicWeapon : EquipmentItem
{
    [Space]
    public float attackCD;
    public float KnockBack;

    [Space]
    public Transform Prefab;

    private void Awake()
    {
        equipmentType = EquipmentType.weapon;
    }

    public override void Attack(int damage)
    {
        if (KeepOnScene.instance.GetComponent<PlayerAttack>().GetAttackCD() <= 0)
        {

            Debug.Log($"Damage done: {damage}"); ///For debug

            Instantiate(Prefab, KeepOnScene.instance.GetComponent<PlayerMovment>().transform.position, Quaternion.identity);

            //Send mesage to Attack animation handler that we use mele weapon
            KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.magic);

            KeepOnScene.instance.GetComponent<PlayerAttack>().SetAttackCD(attackCD);
        }
    }
    public override WeaponType Echo()
    {
        return WeaponType.magic;
    }
}
