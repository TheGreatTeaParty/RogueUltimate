using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private Animator _weaponAnimator;
    private int _prevIndex;
    private PlayerStat _playerStat;
    private Vector3 position;
    private EquipmentItem _currentWeapon;
    private PlayerMovement _playerMovement;

    private int _currentAnimLayer;

    public int PrevIndex => _prevIndex;


    private void Start()
    {
        CharacterManager.Instance.onEquipmentChanged += OnWeaponChanged;
        _weaponAnimator = GetComponent<Animator>();
        _playerStat = PlayerOnScene.Instance.GetComponent<PlayerStat>();
        PlayerOnScene.Instance.playerAttack.onAttacked += OnAttacked;
        PlayerOnScene.Instance.playerAttack.EndAttack += EndAttack;
        position = transform.localPosition;
        _playerMovement = PlayerOnScene.Instance.stats.playerMovement;
    }

    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        _currentWeapon = _new;
        if (_new && _new.EquipmentType == EquipmentType.Weapon)
        {
            if (_old)
                _weaponAnimator.SetLayerWeight((int)_old.AttackAnimationType, 0);
            _weaponAnimator.SetLayerWeight((int)_new.AttackAnimationType, 1);
            _currentAnimLayer = (int)_new.AttackAnimationType;
        }
        else if (!_new && _old.EquipmentType == EquipmentType.Weapon)
        {
            _weaponAnimator.SetLayerWeight((int)_old.AttackAnimationType, 0);
            _currentAnimLayer = 0;
        }
    }
    private void OnAttacked(AttackType attackType)
    {
        if(attackType == AttackType.None)
        {
            return;
        }

        else if(attackType == AttackType.Melee)
        {
            //_weaponAnimator.SetInteger("Set", GenerateAttackSet());
        }
        if (_currentAnimLayer != 0 && _weaponAnimator.GetLayerWeight(_currentAnimLayer) == 0)
        {
            _weaponAnimator.SetLayerWeight(_currentAnimLayer, 1);
        }
        ChangeAnimationSpeed(attackType);

        _weaponAnimator.SetBool("Attack",true);
    }

    private void EndAttack(AttackType attackType)
    {
        if (attackType != AttackType.None)
        {
            _weaponAnimator.SetBool("Attack", false);
            transform.localPosition = position;
        }
    }

    private int GenerateAttackSet()
    {
        if (_prevIndex == 0)
        {
            _prevIndex = 1;
            return _prevIndex;
        }
        else
        {
            _prevIndex = 0;
            return _prevIndex;
        }
    }

    private void ChangeAnimationSpeed(AttackType attackType)
    {
        if (attackType == AttackType.Magic)
            _weaponAnimator.speed = (0.66f / _playerStat.CastSpeed.Value);       //0.5 base attack animation duration in seconds
        else
        {
            _weaponAnimator.speed = (0.66f / _playerStat.AttackSpeed.Value);
        }
    }
    public void GenerateAttackFX()
    {
        if (_currentWeapon)
            if (_currentWeapon.AttackEffect)
            {
                 Transform effect = Instantiate(_currentWeapon.AttackEffect, transform.position + _playerMovement.GetDirection().normalized/2
                     + new Vector3(0,0.5f,0), Quaternion.identity);
                if (_playerMovement.GetDirection().x < 0)
                {
                    effect.localScale = new Vector3(1, -1, 1);
                    effect.transform.rotation = Quaternion.FromToRotation(Vector3.right, _playerMovement.GetDirection().normalized);
                }
                else
                {
                    effect.localScale = new Vector3(1, 1, 1);
                    effect.rotation = Quaternion.FromToRotation(Vector3.right, _playerMovement.GetDirection().normalized);
                }
            }
    }
}
