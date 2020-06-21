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
        equipmentType = EquipmentType.Weapon;
    }

    public override void Attack(int ph_dmg,int mg_dmg)
    {
        Vector3 direction = new Vector3(InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection().x, InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection().y);
        Transform magic = Instantiate(Prefab, KeepOnScene.instance.GetComponent<PlayerMovment>().transform.position + direction, Quaternion.identity);
        magic.GetComponent<FlyingObject>().SetData(ph_dmg, mg_dmg, direction);
        //Send mesage to Attack animation handler that we use Melee Weapon
        KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.Magic);
    }
    public override WeaponType Echo()
    {
        return WeaponType.Magic;
    }

    public override float GetAttackCD()
    {
        return castTime;
    }
}
