using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum WeaponType 
{ 
    Melee, 
    Range, 
    Magic, 
    None 
}


[CreateAssetMenu(menuName = "Items/MeleeWeapon")]
public class MeleeWeapon : EquipmentItem
{
    [Space]
    public float attackCoolDown;
    public float attackRange;
    public float knockBack;
    public float pushForce;

    [Space]
    [SerializeField] private int requiredStamina;
    [SerializeField] private float attackDuration = 0.5f;

    private LayerMask _whatIsEnemy;
    private Vector2 _attackPosition;
    

    public override void Attack(float physicalDamage, float magicDamage)
    {
        // Checks if current stamina is less than required. If not - continues attack.
        if (PlayerStat.Instance.ModifyStamina(requiredStamina) == false)
            return;
        
        _whatIsEnemy = LayerMask.GetMask("Enemy");
        Vector3 direction = KeepOnScene.Instance.playerMovement.GetDirection();
        _attackPosition = KeepOnScene.Instance.transform.position + direction;

        if (KeepOnScene.Instance.playerAttack.GetAttackCD() <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRange, _whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            { 
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(physicalDamage, magicDamage);
            }

            //Send mesage to Attack animation handler that we use Melee Weapon
            KeepOnScene.Instance.playerAttack.onAttacked?.Invoke(WeaponType.Melee, Random.Range(0, 2));
            KeepOnScene.Instance.playerAttack.SetRange(attackRange);
        }
        
    }
    
    public override WeaponType Echo()
    {
        return WeaponType.Melee;
    }

    public override float GetAttackCD()
    {
        return attackCoolDown;
    }
    
}
