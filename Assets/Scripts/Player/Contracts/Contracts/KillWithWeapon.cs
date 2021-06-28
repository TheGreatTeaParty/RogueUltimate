using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Contracts/KillTemplate")]
public class KillWithWeapon : Contract
{
    [HideInInspector]
    
    public AttackType WeaponType;

    private Equipment equipment;

    public override void StartContract()
    {
        equipment = CharacterManager.Instance.Equipment;
        CharacterManager.Instance.Stats.OnKillChanged += UpdateKills;
    }

    private void UpdateKills()
    {
        EquipmentItem weapon = equipment.equipmentSlots[5].Item as EquipmentItem;
        if (weapon.Echo() == WeaponType)
            _currentScore++;
    }
}
