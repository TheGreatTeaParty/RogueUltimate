using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/MeleeWeapon")]

public class MeleeWeapon : EquipmentItem
{
    [Space]
    public float attackCoolDown;
    public float attackRange;
    public float KnockBack;
    public float pushForce;

    private LayerMask whatIsEnemy;
    private Vector2 attackPosition;
    
    public override void Attack(int ph_damage, int mg_damage)
    {
        whatIsEnemy = LayerMask.GetMask("Enemy");
        Vector3 direction = KeepOnScene.instance.GetComponent<PlayerMovment>().GetDirection();
        attackPosition = KeepOnScene.instance.transform.position + direction / 2;

        if (KeepOnScene.instance.GetComponent<PlayerAttack>().GetAttackCD() <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            { 
                enemiesToDamage[i].GetComponent<Transform>().position += direction * KnockBack;
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(ph_damage, mg_damage);
            }

            //Send mesage to Attack animation handler that we use Melee Weapon
            KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.Melee);
            KeepOnScene.instance.GetComponent<PlayerAttack>().SetRange(attackRange);
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


public enum WeaponType 
{ 
    Melee, 
    Range, 
    Magic, 
    None 
}
