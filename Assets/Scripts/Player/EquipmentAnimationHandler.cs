using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EquipmentAnimationHandler : MonoBehaviour
{
    public Animator WeaponAnim;
    public Animator EquipmentAnim;

    private AnimatorController WeaponController;
    private AnimatorController EquipmentController;
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
        //WeaponAnim.SetFloat("Horizontal", direction.x);
        //WeaponAnim.SetFloat("Vertical", direction.y);

        //EquipmentAnim.SetFloat("Horizontal", direction.x);
        //EquipmentAnim.SetFloat("Vertical", direction.y);
    }

    //When the equipment changed, change the Animation controller
    private void OnWeaponChanged(EquipmentItem _new, EquipmentItem _old)
    {
        if (_new != null)
            if (_new.equipmentType == EquipmentType.Weapon)
            {
                WeaponController = _new.EquipmentAnimations;
                WeaponAnim.runtimeAnimatorController = WeaponController as RuntimeAnimatorController;
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
    private void AttackAnimation(WeaponType type)
    {
        if(WeaponAnim != null)
            WeaponAnim.SetTrigger("Attack");
        if(EquipmentAnim != null)
            EquipmentAnim.SetTrigger("Attack");
    }
}
