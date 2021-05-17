using System.Collections;
using UnityEngine;
using System;


public class PlayerAttack : MonoBehaviour
{
    public float FistAttackSpeed = 0.9f;
    public float FistAttackRange = 0.3f;
    public float FistPushForce = 1.2f;
    public float FistStaminaCost = -10;

    public float CurrentAttackCD => _currentAttackCD;

    private float _currentAttackCD = 0;
    private IEnumerator attackCoroutine;
    private float _playerAttackSpeed = 0;
    private bool _isAttacking = false;
    private LayerMask _whatIsEnemy;
    private Vector3 _direction;

    //Deligate to trigger animation
    public delegate void OnAttacked(AttackType type);
    public OnAttacked onAttacked;

    public Action<AttackType> EndAttack;
    public bool AllowedToAttack = true;
    // Cache
    private PlayerStat _playerStat;
    private PlayerMovement _playerMovement;


    private void Awake()
    {
        _whatIsEnemy = LayerMask.GetMask("Enemy","EnvObjects");
    }

    private void Start()
    {
        // Cache
        _playerStat = GetComponent<PlayerStat>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _direction = _playerMovement.GetDirection();
    }


    public void Attack()
    {
        if (AllowedToAttack)
        {
            if (_playerAttackSpeed != _playerStat.AttackSpeed.Value)
                _playerAttackSpeed = _playerStat.AttackSpeed.Value;

            Equipment equipment = CharacterManager.Instance.Equipment;
            EquipmentItem weapon = equipment.equipmentSlots[5].Item as EquipmentItem;

            if (!_isAttacking && !_playerMovement.IsStopped())
            {
                _isAttacking = true;

                //Calculate attack time depanding on the item type:
                if (weapon && weapon.Echo() == AttackType.Magic)
                    _currentAttackCD = _playerStat.CastSpeed.Value;
                else
                    _currentAttackCD = _playerStat.AttackSpeed.Value;

                //Start Attack enumirator:
                attackCoroutine = AttackAnimationWait(weapon);
                StartCoroutine(attackCoroutine);

            }
        }
    }

    private void FistAttack()
    {
        if (!_playerStat.ModifyStamina(FistStaminaCost)) return;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(
            transform.position + _direction / 2, FistAttackRange, _whatIsEnemy.value);

        for (int i = 0; i < enemiesToDamage.Length; i++)
            enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(_playerStat.PhysicalDamage.Value, _playerStat.MagicDamage.Value);
        if(enemiesToDamage.Length > 0)
            ScreenShakeController.Instance.StartShake(0.05f, 0.03f);
        _playerMovement.PushToDirection(FistPushForce);
        PlayerStop(_currentAttackCD);
        EndAttack?.Invoke(AttackType.None);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PlayerOnScene.Instance.playerMovement.PlayerCollider.bounds.center + _direction.normalized/1.5f, _playerStat.AttackRange.Value);
    }

    private IEnumerator AttackAnimationWait(EquipmentItem weapon)
    {
        if (weapon)
        {
            if (CheckRequiredStats(weapon)) ///Check and use stats
            {

                onAttacked?.Invoke(weapon.Echo());

                if (weapon.Echo() == AttackType.Melee)
                {
                    _playerMovement.PushToDirection(_playerStat.PushForce.Value);
                    PlayerStop(_currentAttackCD*0.9f);
                }

                yield return new WaitForSeconds(_currentAttackCD - CurrentAttackCD*0.2f);

                weapon.Attack(_playerStat.PhysicalDamage.Value, _playerStat.MagicDamage.Value);
                EndAttack?.Invoke(weapon.Echo());
                _isAttacking = false;
            }
            _isAttacking = false;
        }

        else
        {
            if (_playerStat.CheckStamina(FistStaminaCost))
                onAttacked?.Invoke(AttackType.None);

            yield return new WaitForSeconds(FistAttackSpeed - FistAttackSpeed*0.25f);
            FistAttack();
            _isAttacking = false;
        }

    }

    void PlayerStop(float time)
    {
        StartCoroutine(_playerMovement.DisablePlayerControll(time));
    }

    public void StopAttack()
    {
        StopCoroutine(attackCoroutine);
        EndAttack?.Invoke(AttackType.Range);
        _isAttacking = false;
    }

    private bool CheckRequiredStats(EquipmentItem weapon)
    {
        switch (weapon.Echo())
        {
            case AttackType.Melee:
                {
                    MeleeWeapon mele = weapon as MeleeWeapon;
                    if (_playerStat.CheckHealth(mele.RequiredHealth) &&
                        _playerStat.CheckStamina(mele.RequiredStamina) &&
                        _playerStat.CheckMana(mele.RequiredMana))
                        return true;
                    else
                        return false;
                }
            case AttackType.Range:
                {
                    RangeWeapon range = weapon as RangeWeapon;
                    if (_playerStat.CheckHealth(range.RequiredHealth) &&
                        _playerStat.CheckStamina(range.RequiredStamina) &&
                        _playerStat.CheckMana(range.RequiredMana))
                        return true;
                    else
                        return false;
                }
            case AttackType.Magic:
                {
                    MagicWeapon magic = weapon as MagicWeapon;
                    if (_playerStat.CheckHealth(magic.RequiredHealth) &&
                        _playerStat.CheckStamina(magic.RequiredStamina) &&
                        _playerStat.CheckMana(magic.RequiredMana))
                        return true;
                    else
                        return false;
                }
            default:
                return false;
        }
    }
}
