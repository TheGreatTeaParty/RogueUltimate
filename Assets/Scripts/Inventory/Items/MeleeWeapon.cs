using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/MeleeWeapon")]

public class MeleeWeapon : EquipmentItem
{
    [Space]
    public float attackCD;
    public float attackRange;
    public float KnockBack;
    public float pushForce;

    private LayerMask whatIsEnemy;
    private Vector2 attackPos;

    private void Awake()
    {
        equipmentType = EquipmentType.weapon;
        whatIsEnemy = LayerMask.NameToLayer("Enemy");
    }
    
    public override void Attack(int damage)
    {
        Vector3 direction = KeepOnScene.instance.GetComponent<PlayerMovment>().GetDirection();
        attackPos = KeepOnScene.instance.transform.position + direction/2;

        if (KeepOnScene.instance.GetComponent<PlayerAttack>().GetAttackCD() <= 0)
        {
            /*Push the player to the direction of the hit
            Vector2 push_direction = new Vector2(direction.x * pushForce, direction.y * pushForce);
            KeepOnScene.instance.GetComponent<PlayerMovment>().Push(push_direction); */

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos, attackRange, whatIsEnemy);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<CharacterStat>().TakeDamage(damage);
                /*enemiesToDamage[i].GetComponent<EnemyStats>().AddForce(GetComponent<MoveScript>().ReturnDirection() * KnockBack);
                This is to knock back the enemies
                */
            }
            Debug.Log($"Damage done: {damage}"); ///For debug

            //Send mesage to Attack animation handler that we use mele weapon
            KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked?.Invoke(WeaponType.mele);

            KeepOnScene.instance.GetComponent<PlayerAttack>().SetAttackCD(attackCD);

            KeepOnScene.instance.GetComponent<PlayerAttack>().SetRange(attackRange);
        }
    }
    public override WeaponType Echo()
    {
        return WeaponType.mele;
    }
}
public enum WeaponType { mele, range, magic, nothing };
