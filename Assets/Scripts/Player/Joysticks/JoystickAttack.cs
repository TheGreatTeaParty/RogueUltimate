using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAttack : MonoBehaviour
{
    protected Joystick joystick;

    private Vector2 movement;
    private bool _isShooting;
    private bool _isSlowed;
    private float timer = 0;
    private bool playin_audio = false;

    public void Start()
    {
        _isShooting = false;
        _isSlowed = false;
        joystick = GetComponent<Joystick>();
    }

    /*There we receive input information*/
    void Update()
    {
        movement = joystick.Direction;
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            _isShooting = true;

            //if not slowed, slow the character down
            if (!_isSlowed)
            {
                KeepOnScene.instance.GetComponent<PlayerMovment>().SlowDown(0.5f);
                _isSlowed = true;
            }

            if (!playin_audio)
            {
                playin_audio = true;

                RangeWeapon weapon = EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon] as RangeWeapon;
                if (weapon == null)
                {
                    MagicWeapon MagicWeapon = EquipmentManager.Instance.currentEquipment[(int)EquipmentType.Weapon] as MagicWeapon;
                    KeepOnScene.instance.GetComponent<AudioSource>().PlayOneShot(MagicWeapon.PrepareSound);
                }
                else
                {
                    KeepOnScene.instance.GetComponent<AudioSource>().PlayOneShot(weapon.PrepareSound);
                }
            }

            if (_isShooting)
            {
                timer += Time.deltaTime;
            }

            if (_isShooting && timer > KeepOnScene.instance.GetComponent<PlayerAttack>().GetWeaponCD())
            {
                if (movement.x != 0 || movement.y != 0)
                {
                    KeepOnScene.instance.GetComponent<PlayerAttack>().Attack();
                }

                playin_audio = false;
                _isShooting = false;
                timer = 0;
            }
        }

        //Return the normal speed;
        else if(_isSlowed)
        {
            KeepOnScene.instance.GetComponent<PlayerMovment>().SlowDown(2f);
            _isSlowed = false;
            playin_audio = false;
            timer = 0;
        }
    }

    public Vector2 GetDirection()
    {
        return movement.normalized;
    }
}
