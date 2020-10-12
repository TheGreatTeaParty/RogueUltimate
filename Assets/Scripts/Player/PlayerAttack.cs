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
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        
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
        Equipment equipment = CharacterManager.Instance.Equipment;
        EquipmentItem weapon = equipment.equipmentSlots[(int)EquipmentType.Weapon].Item as EquipmentItem;
        
        if (!(_startAttackCoolDown <= 0)) return;
        
        //If there is any Weapon call the function attack there

        if (weapon != null) 
        {
            weapon.Attack(_playerStat.physicalDamage.Value, _playerStat.magicDamage.Value);
                
            if (weapon.Echo() == WeaponType.Melee)
            {
                StartCoroutine(PlayerStop(0.5f));
                onAttacked?.Invoke(WeaponType.Melee, 1);
            }

            _startAttackCoolDown = _weaponCoolDown;
        }
        //if not, this is base fist attack
        else
        {
            Debug.Log("No weapon equipped");
            FistAttack();   
        }

    }
    
    private void FistAttack()
    {
        if (!(_startAttackCoolDown <= 0)) return;
        
        attackRange = _fistRange;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(
            transform.position + _direction / 2, attackRange, _whatIsEnemy.value);

        for (int i = 0; i < enemiesToDamage.Length; i++)
            enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(_playerStat.physicalDamage.Value, _playerStat.magicDamage.Value);
        
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
        PlayerOnScene.Instance.playerMovement.StopMoving();
        yield return new WaitForSeconds(attackDuration);
        PlayerOnScene.Instance.playerMovement.StartMoving();
    }
    
}
