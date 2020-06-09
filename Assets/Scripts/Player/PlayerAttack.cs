using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public float attackCoolDown;
    public float attackRange;
    public float knockBack;
    public float pushForce;

    private float startAttackCoolDown;
    private float weaponCoolDown;
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
        if (startAttackCoolDown > 0)
            startAttackCoolDown -= Time.deltaTime;
        
        direction = GetComponent<PlayerMovment>().GetDirection();
    }
    

    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new != null) 
            if (_new.equipmentType == EquipmentType.Weapon)
                weaponCoolDown = _new.GetAttackCD();
    }
    

    public void Attack()
    {
        //If there is any Weapon call the function attack there
        if (EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon] != null)
        {
            EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon].Attack(GetComponent<PlayerStat>().physicalDamage.GetValue(), GetComponent<PlayerStat>().magicDamage.GetValue());
            startAttackCoolDown = weaponCoolDown;
        }
        //if not, this is base fist attack
        else
        {
            FistAttack();
        }
    }

    
    private void FistAttack()
    {
        if (startAttackCoolDown <= 0)
        {
            attackRange = fistRange;

            /*Push the player to the direction of the hit
            Vector2 push_direction = new Vector2(direction.x * pushForce, direction.y * pushForce);
            GetComponent<PlayerMovment>().Push(push_direction); */

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position + direction / 2, attackRange, whatIsEnemy.value);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(GetComponent<PlayerStat>().physicalDamage.GetValue(), GetComponent<PlayerStat>().magicDamage.GetValue());
                /*enemiesToDamage[i].GetComponent<EnemyStats>().AddForce(GetComponent<MoveScript>().ReturnDirection() * knockBack);
                This is to knock back the enemies
                */
            }
            startAttackCoolDown = attackCoolDown;
            onAttacked?.Invoke(WeaponType.None);
        }
    }

    
    public float GetAttackCD()
    {
        return startAttackCoolDown;
    }

    
    public float GetWeaponCD()
    {
        return weaponCoolDown;
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
