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
    public delegate void OnAttacked(WeaponType type,int set);
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
        if (startAttackCoolDown <= 0)
        {
            //If there is any Weapon call the function attack there
            if (EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon] != null)
            {
                EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon].Attack(GetComponent<PlayerStat>().physicalDamage.GetValue(), GetComponent<PlayerStat>().magicDamage.GetValue());
                if(EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon].Echo() == WeaponType.Melee)
                {
                    StartCoroutine(PlayerStop(0.5f));
                }
                
                startAttackCoolDown = weaponCoolDown;
            }
            //if not, this is base fist attack
            else
            {
                FistAttack();
            }
        }
    }

    
    private void FistAttack()
    {
        if (startAttackCoolDown <= 0)
        {
            attackRange = fistRange;

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position + direction / 2, attackRange, whatIsEnemy.value);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(GetComponent<PlayerStat>().physicalDamage.GetValue(), GetComponent<PlayerStat>().magicDamage.GetValue());
            }
            startAttackCoolDown = attackCoolDown;
            StartCoroutine(PlayerStop(0.4f));
            onAttacked?.Invoke(WeaponType.None,0);
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

    IEnumerator PlayerStop(float attackDuration)
    {
        KeepOnScene.instance.GetComponent<PlayerMovment>().StopMoving();
        yield return new WaitForSeconds(attackDuration);
        KeepOnScene.instance.GetComponent<PlayerMovment>().StartMoving();
    }
}
