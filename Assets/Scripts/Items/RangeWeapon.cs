﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/RangeWeapon")]

public class RangeWeapon : EquipmentItem
{
    [Space]
    public float attackCD;
    public float KnockBack;

    [Space]
    [SerializeField] private int requiredStamina;
    
    [Space]
    public Transform arrowPrefab;

    
    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }

    public override void Attack(int ph_dmg, int mg_dmg)
    {
        // Checks if current stamina is less than required. If not - continues attack.
        if (PlayerStat.Instance.ModifyStamina(requiredStamina) == false)
            return;
            
        Vector3 direction = new Vector3(InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection().x, InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection().y);
        Transform arrow = Instantiate(arrowPrefab,KeepOnScene.instance.GetComponent<PlayerMovment>().transform.position + direction, Quaternion.identity);
        arrow.GetComponent<FlyingObject>().SetData(ph_dmg, mg_dmg, direction);
        //Send mesage to Attack animation handler that we use Melee Weapon
        KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.Range,0);
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
