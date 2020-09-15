using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class EquipmentAnimationHandler : MonoBehaviour
{
    public Animator weaponAnimator;
    public Animator equipmentAnimator;

    private RuntimeAnimatorController _weaponController;
    private RuntimeAnimatorController _equipmentController;
    private Vector2 _direction;
    private PlayerMovement _playerMovement;
  
    
    private void Start()
    {
        _playerMovement = KeepOnScene.Instance.GetComponent<PlayerMovement>();

        EquipmentManager.Instance.onEquipmentChanged += OnWeaponChanged;
        KeepOnScene.Instance.playerAttack.onAttacked += AttackAnimation;

        if (EquipmentManager.Instance != null)
            EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }
    
    private void Update()
    {
        if (_weaponController is null) return;
     
        _direction = _playerMovement.GetDirection();
        int x = 0;
        int y = 0;

        if (_direction.x > 0.5f)
        {
            x = 1;
            y = 0;
            weaponAnimator.SetFloat("Horizontal", x);
            weaponAnimator.SetFloat("Vertical", y);
        }
        else if (_direction.x < -0.5f)
        {
            x = -1;
            y = 0;
            weaponAnimator.SetFloat("Horizontal", x);
            weaponAnimator.SetFloat("Vertical", y);
        }
        else
        {
            weaponAnimator.SetFloat("Horizontal", _direction.x);
            weaponAnimator.SetFloat("Vertical", _direction.y);
        }

        //EquipmentAnim.SetFloat("Horizontal", direction.x);
        //EquipmentAnim.SetFloat("Vertical", direction.y);
    }

    //When the equipment changed, change the Animation controller
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new != null)
        {
            if (_new.equipmentType == EquipmentType.Weapon)
            {
                _weaponController = _new.EquipmentAnimations;
                weaponAnimator.runtimeAnimatorController = _weaponController as RuntimeAnimatorController;
            }
        }
        //If we drop the weapon, clear the animation controller
        else
        {
            weaponAnimator.gameObject.transform.rotation = Quaternion.identity;
            weaponAnimator.runtimeAnimatorController = null as RuntimeAnimatorController;
            _weaponController = null;
        }
    }

    private void OnEquipmentChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new != null)
            if (_new.equipmentType == EquipmentType.Armor)
            {
                _equipmentController = _new.EquipmentAnimations;
                equipmentAnimator.runtimeAnimatorController = _equipmentController as RuntimeAnimatorController;
            }
    }

    //When attack, trigger the Attack animation
    private void AttackAnimation(WeaponType type,int set)
    {
        if (_weaponController != null)
        {
            weaponAnimator.SetTrigger("Attack");
            weaponAnimator.SetInteger("Set", set);
        }
        if(_equipmentController != null)
            equipmentAnimator.SetTrigger("Attack");
    }
    public void RotateRangeWeapon(Vector3 dir)
    {
        weaponAnimator.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }
}
