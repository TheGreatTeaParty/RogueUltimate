using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAttack : MonoBehaviour
{
    protected Joystick joystick;
    private Vector2 movement;

    public void Start()
    {
        joystick = GetComponent<Joystick>();
    }

    /*There we receive input information*/
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        KeepOnScene.instance.GetComponent<PlayerAttack>().Attack();
    }
    public Vector2 GetDirection()
    {
        return movement;
    }
}
