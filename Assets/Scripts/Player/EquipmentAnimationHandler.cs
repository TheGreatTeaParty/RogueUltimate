using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class EquipmentAnimationHandler : MonoBehaviour
{
    public Animator WeaponAnim;
    public Animator EquipmentAnim;

    private RuntimeAnimatorController WeaponController;
    private RuntimeAnimatorController EquipmentController;
    private Vector2 direction;
    private PlayerMovment playerMovment;
  
    private void Start()
    {
        playerMovment = KeepOnScene.instance.GetComponent<PlayerMovment>();

        EquipmentManager.Instance.onEquipmentChanged += OnWeaponChanged;
        KeepOnScene.instance.GetComponent<PlayerAttack>().onAttacked += AttackAnimation;

        if (EquipmentManager.Instance != null)
            EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = playerMovment.GetDirection();
        if (WeaponController != null)
        {
            int x = 0;
            int y = 0;

            if (direction.x > 0.5f)
            {
                x = 1;
                y = 0;
                WeaponAnim.SetFloat("Horizontal", x);
                WeaponAnim.SetFloat("Vertical", y);
            }
            else if (direction.x < -0.5f)
            {
                x = -1;
                y = 0;
                WeaponAnim.SetFloat("Horizontal", x);
                WeaponAnim.SetFloat("Vertical", y);
            }
            else
            {
                WeaponAnim.SetFloat("Horizontal", direction.x);
                WeaponAnim.SetFloat("Vertical", direction.y);
            }
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
                WeaponController = _new.EquipmentAnimations;
                WeaponAnim.runtimeAnimatorController = WeaponController as RuntimeAnimatorController;
            }
        }
        //If we drop the weapon, clear the animation controller
        else
        {
            WeaponAnim.runtimeAnimatorController = null as RuntimeAnimatorController;
            WeaponController = null;
        }
    }

    private void OnEquipmentChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new != null)
            if (_new.equipmentType == EquipmentType.Armor)
            {
                EquipmentController = _new.EquipmentAnimations;
                EquipmentAnim.runtimeAnimatorController = EquipmentController as RuntimeAnimatorController;
            }
    }

    //When attack, trigger the Attack animation
    private void AttackAnimation(WeaponType type,int set)
    {
        if (WeaponController != null)
        {
            WeaponAnim.SetTrigger("Attack");
            WeaponAnim.SetInteger("Set", set);
        }
        if(EquipmentController != null)
            EquipmentAnim.SetTrigger("Attack");
    }
}
