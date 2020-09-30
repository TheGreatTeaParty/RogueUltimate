using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAttack : MonoBehaviour
{
    protected Joystick joystick;

    private Vector2 _movement;
    private bool _isShooting;
    private bool _isSlowed;
    private float _timer = 0;
    private bool _audioIsPlaying = false;
    
    // Cache
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private AudioSource _audioSource;
    private Equipment _equipment;
    
    
    public void Start()
    {
        _isShooting = false;
        _isSlowed = false;
        joystick = GetComponent<Joystick>();
        
        // Cache
        _playerMovement = KeepOnScene.Instance.playerMovement;
        _playerAttack = KeepOnScene.Instance.playerAttack;
        _audioSource = KeepOnScene.Instance.audioSource;
        _equipment = CharacterManager.Instance.Equipment;
    }

    /*There we receive input information*/
    private void Update()
    {
        _movement = joystick.Direction;
        KeepOnScene.Instance.equipmentAnimationHandler.RotateRangeWeapon(_movement.normalized);
    }

    private void FixedUpdate()
    {
        if (_movement.x != 0 || _movement.y != 0)
        {
            _isShooting = true;

            //if not slowed, slow the character down
            if (!_isSlowed)
            {
                _playerMovement.SetRangeJoystick(this);
                _playerMovement.SlowDown(0.5f);
                _playerMovement.SetRangeMoving(true);
                _isSlowed = true;
            }

            if (!_audioIsPlaying)
            {
                _audioIsPlaying = true;
                
                RangeWeapon weapon = _equipment.equipmentSlots[(int)EquipmentType.Weapon].Item as RangeWeapon;
                if (weapon == null)
                {
                    MagicWeapon MagicWeapon = _equipment.equipmentSlots[(int)EquipmentType.Weapon].Item as MagicWeapon;
                    _audioSource.PlayOneShot(MagicWeapon.prepareSound);
                }
                else
                {
                    _audioSource.PlayOneShot(weapon.prepareSound);
                }
                _playerAttack.onAttacked?.Invoke(WeaponType.Melee, 0);
            }

            if (_isShooting)
                _timer += Time.deltaTime;

            if (_isShooting && _timer > _playerAttack.GetWeaponCD())
            {
                if (_movement.x != 0 || _movement.y != 0)
                {
                    _playerAttack.Attack();
                    _playerAttack.onAttacked?.Invoke(WeaponType.Melee, 1);
                }
            
                _audioIsPlaying = false;
                _isShooting = false;
                _timer = 0;
            }
        }

        //Return the normal speed;
        else if (_isSlowed)
        {
            _playerMovement.SlowDown(2f);
            _playerMovement.SetRangeMoving(false);
            _playerAttack.onAttacked?.Invoke(WeaponType.Range, 1);
            _isSlowed = false;
            _audioIsPlaying = false;
            _timer = 0;
        }
    }

    public Vector2 GetDirection()
    {
        return _movement.normalized;
    }
    
}
