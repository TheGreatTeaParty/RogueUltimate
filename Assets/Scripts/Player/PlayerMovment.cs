using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour 
{
    [SerializeField] private float movement_speed = 10.0f;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator; 
    [SerializeField] protected Joystick joystick;
    private Vector2 movement;
    private Vector2 direction;

    public void Start()
    {
        joystick = InterfaceOnScene.instance.GetComponentInChildren<Joystick>();
    }

    /*There we receive input information*/
    void Update() 
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        //Save the direction of player movement
        if (movement.x != 0 || movement.y != 0)
            direction = movement;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
    

    /*I called func. moveCharacter there, because FixedUpdate is better for physic detection*/
    void FixedUpdate() 
    {
        moveCharacter(movement);
    }
    
    
    /*This is the main function which calculate Character's movement*/
    void moveCharacter(Vector2 movement_direction)
    {
        rb2D.MovePosition((Vector2)transform.position + 
                                    (movement_speed * 
                                    movement_direction * 
                                        Time.deltaTime));
    }

    public void Push(Vector2 push_direction)
    {
        rb2D.AddForce(push_direction, ForceMode2D.Impulse);
    }
    
    public void attackCharacter(){

        animator.SetTrigger("Attack");

    }
    public Vector3 GetDirection()
    {
        return direction;
    }

}
