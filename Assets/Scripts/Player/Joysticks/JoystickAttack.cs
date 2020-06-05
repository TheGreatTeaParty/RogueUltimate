using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAttack : MonoBehaviour
{
    protected Joystick joystick;

    private Vector2 movement;
    private bool _isShooting;
    private float timer = 0;

    public void Start()
    {
        _isShooting = false;
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

            if (_isShooting)
                timer += Time.deltaTime;
            if (_isShooting)
                KeepOnScene.instance.GetComponent<PlayerMovment>().SlowDown(0.5f);

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
        else if (_isShooting && movement.x == 0 && movement.y == 0)
        {
            timer = 0;
            KeepOnScene.instance.GetComponent<PlayerMovment>().SlowDown(1f);
        }
    }

    public Vector2 GetDirection()
    {
        return movement;
    }
}
