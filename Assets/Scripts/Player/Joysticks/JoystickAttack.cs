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

            if (_isShooting)
                timer += Time.deltaTime;

            if (_isShooting && timer > KeepOnScene.instance.GetComponent<PlayerAttack>().GetWeaponCD())
            {
                if (movement.x != 0 || movement.y != 0)
                {
                    KeepOnScene.instance.GetComponent<PlayerAttack>().Attack();
                }


                _isShooting = false;
                timer = 0;
            }
        }

        //Return the normal speed;
        else if(_isSlowed)
        {
            KeepOnScene.instance.GetComponent<PlayerMovment>().SlowDown(2f);
            _isSlowed = false;
        }
    }

    public Vector2 GetDirection()
    {
        return movement.normalized;
    }
}
