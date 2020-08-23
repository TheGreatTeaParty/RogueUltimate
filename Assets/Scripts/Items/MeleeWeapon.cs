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

    [Space]
    [SerializeField] private int requiredStamina;

    private LayerMask whatIsEnemy;
    private Vector2 attackPosition;
    
    
    public override void Attack(int ph_damage, int mg_damage)
    {
        // Checks if current stamina is less than required. If not - continues attack.
        if (PlayerStat.Instance.ModifyStamina(requiredStamina) == false)
            return;
        
        whatIsEnemy = LayerMask.GetMask("Enemy");
        Vector3 direction = KeepOnScene.instance.GetComponent<PlayerMovment>().GetDirection();
        attackPosition = KeepOnScene.instance.transform.position + direction;

        if (KeepOnScene.instance.GetComponent<PlayerAttack>().GetAttackCD() <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            { 
                enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(direction * KnockBack,ForceMode2D.Impulse);
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(ph_damage, mg_damage);
            }

            //Send mesage to Attack animation handler that we use Melee Weapon
            KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.Melee, Random.Range(0, 2));
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
