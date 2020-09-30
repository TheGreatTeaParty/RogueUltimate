using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/RangeWeapon")]

public class RangeWeapon : EquipmentItem
{
    [Space]
    public float attackCD;
    public float knockBack;
    [Space]
    [SerializeField] private int requiredStamina;
    [Space]
    public Transform arrowPrefab;
    [Space]
    public AudioClip prepareSound;
    
    
    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }

    public override void Attack(float physicalDamage, float magicDamage)
    {
        // Checks if current stamina is less than required. If not - continues attack.
        if (PlayerStat.Instance.ModifyStamina(requiredStamina) == false)
            return;
            
        Vector3 direction = new Vector3(
            InterfaceManager.Instance.joystickAttack.GetDirection().x, 
            InterfaceManager.Instance.joystickAttack.GetDirection().y);
        Transform arrow = Instantiate(arrowPrefab,
            KeepOnScene.Instance.playerMovement.transform.position + direction, Quaternion.identity);
        arrow.GetComponent<FlyingObject>().SetData(physicalDamage, magicDamage, direction);
        
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
