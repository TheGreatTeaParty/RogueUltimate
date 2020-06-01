using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCD;
    public float attackRange;
    public float KnockBack;
    public float pushForce;

    private float StartattackCD;
    private float fistRange;
    private LayerMask whatIsEnemy;
    private Vector3 direction;

    //Deligate to trigger animation
    public delegate void OnAttacked(WeaponType type);
    public OnAttacked onAttacked;

    private void Awake()
    {
        whatIsEnemy.value = LayerMask.NameToLayer("Enemy");
        fistRange = attackRange;
    }

    private void Update()
    {
        if (StartattackCD > 0)
            StartattackCD -= Time.deltaTime;
        direction = GetComponent<PlayerMovment>().GetDirection();
    }

    public void Attack()
    {

        //If there is any weapon call the function attack there
        if (EquipmentManager.instance.currentEquipment[(int)EquipmentType.weapon] != null)
        {
            EquipmentManager.instance.currentEquipment[(int)EquipmentType.weapon].Attack(GetComponent<PlayerStat>().damage.GetValue());
        }

        //if not, this is base fist attack
        else
        {

            FistAttack();
        }
    }

    private void FistAttack()
    {
        if (StartattackCD <= 0)
        {
            attackRange = fistRange;

            /*Push the player to the direction of the hit
            Vector2 push_direction = new Vector2(direction.x * pushForce, direction.y * pushForce);
            GetComponent<PlayerMovment>().Push(push_direction); */

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position + direction / 2, attackRange, whatIsEnemy);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<CharacterStat>().TakeDamage(GetComponent<PlayerStat>().damage.GetValue());
                /*enemiesToDamage[i].GetComponent<EnemyStats>().AddForce(GetComponent<MoveScript>().ReturnDirection() * KnockBack);
                This is to knock back the enemies
                */
            }
            StartattackCD = attackCD;
            onAttacked?.Invoke(WeaponType.nothing);
        }
    }

    public float GetAttackCD()
    {
        return StartattackCD;
    }

    public void SetAttackCD(float _cd)
    {
        StartattackCD = _cd;
    }

    public void SetRange(float range)
    {
        attackRange = range;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + direction/2, attackRange);
    }
}
