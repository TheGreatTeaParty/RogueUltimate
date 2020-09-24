using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public float attackCoolDown;
    public float attackRange;
    public float knockBack;
    public float pushForce;

    private float _startAttackCoolDown;
    private float _weaponCoolDown;
    private float _fistRange;
    private LayerMask _whatIsEnemy;
    private Vector3 _direction;

    //Deligate to trigger animation
    public delegate void OnAttacked(WeaponType type,int set);
    public OnAttacked onAttacked;

    // Cache
    private PlayerStat _playerStat;
    private PlayerMovement _playerMovement;
    
    
    private void Awake()
    {
        _whatIsEnemy = LayerMask.GetMask("Enemy");
        _fistRange = attackRange;
    }

    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnWeaponChanged;
        
        // Cache
        _playerStat = GetComponent<PlayerStat>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_startAttackCoolDown > 0)
            _startAttackCoolDown -= Time.deltaTime;
        
        _direction = _playerMovement.GetDirection();
    }

    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new != null) 
            if (_new.equipmentType == EquipmentType.Weapon)
                _weaponCoolDown = _new.GetAttackCD();
    }

    public void Attack()
    {
        if (!(_startAttackCoolDown <= 0)) return;
        
        //If there is any Weapon call the function attack there
        if (EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon] != null)
        {
            EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon].Attack(
                _playerStat.physicalDamage.GetValue(), _playerStat.magicDamage.GetValue());
                
            if(EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon].Echo() == WeaponType.Melee)
            {
                StartCoroutine(PlayerStop(0.5f));
                onAttacked?.Invoke(WeaponType.Melee, 1);
            }

            _startAttackCoolDown = _weaponCoolDown;
        }
        //if not, this is base fist attack
        else
            FistAttack();

    }
    
    private void FistAttack()
    {
        if (!(_startAttackCoolDown <= 0)) return;
        
        attackRange = _fistRange;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(
            transform.position + _direction / 2, attackRange, _whatIsEnemy.value);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(
                _playerStat.physicalDamage.GetValue(), _playerStat.magicDamage.GetValue());
        }
        _startAttackCoolDown = attackCoolDown;
        StartCoroutine(PlayerStop(0.4f));
        onAttacked?.Invoke(WeaponType.None,0);
    }
    
    public float GetAttackCD()
    {
        return _startAttackCoolDown;
    }

    public float GetWeaponCD()
    {
        return _weaponCoolDown;
    }

    public void SetRange(float range)
    {
        attackRange = range;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _direction/2, attackRange);
    }

    IEnumerator PlayerStop(float attackDuration)
    {
        KeepOnScene.Instance.playerMovement.StopMoving();
        yield return new WaitForSeconds(attackDuration);
        KeepOnScene.Instance.playerMovement.StartMoving();
    }
    
}
