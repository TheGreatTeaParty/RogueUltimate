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
    private float weaponCD;
    private float fistRange;
    private LayerMask whatIsEnemy;
    private Vector3 direction;

    //Deligate to trigger animation
    public delegate void OnAttacked(WeaponType type);
    public OnAttacked onAttacked;

    private void Awake()
    {
        whatIsEnemy = LayerMask.GetMask("Enemy");
        fistRange = attackRange;
    }

    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnWeaponChanged;
    }

    private void Update()
    {
        if (StartattackCD > 0)
            StartattackCD -= Time.deltaTime;
        direction = GetComponent<PlayerMovment>().GetDirection();
    }

    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new.equipmentType == EquipmentType.weapon)
        {
            
            if (_new != null)
            {
                weaponCD = _new.GetAttackCD();
            }
        }
    }

    public void Attack()
    {

        //If there is any weapon call the function attack there
        if (EquipmentManager.Instance.currentEquipment[(int)EquipmentType.weapon] != null)
        {
            EquipmentManager.Instance.currentEquipment[(int)EquipmentType.weapon].Attack(GetComponent<PlayerStat>().damage.GetValue());
            StartattackCD = weaponCD;
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

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position + direction / 2, attackRange, whatIsEnemy.value);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(GetComponent<PlayerStat>().damage.GetValue());
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

    public float GetWeaponCD()
    {
        return weaponCD;
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
