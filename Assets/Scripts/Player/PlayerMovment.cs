using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour 
{
    public float movementSpeed;
    public float BASE_MOVEMENT_SPEED;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator; 
    [SerializeField] protected Joystick joystick;
    private Vector2 movementDirection;
    private Vector2 direction;

    public void Start()
    {
        joystick = InterfaceOnScene.Instance.GetComponentInChildren<Joystick>();
    }

    /*There we receive input information*/
    void Update() 
    {
        ProcessInputs();

        //Save the direction of player movement
        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            direction = movementDirection;
        }
        animator.SetFloat("Speed", movementSpeed);

    }
    

    /*I called func. moveCharacter there, because FixedUpdate is better for physic detection*/
    void FixedUpdate() 
    {
        MoveCharacter();
    }
    
    void ProcessInputs()
    {
        movementDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

    }
    
    /*This is the main function which calculates Character's movement*/
    void MoveCharacter()
    {
        rb2D.MovePosition((Vector2)transform.position + 
                            (movementSpeed * BASE_MOVEMENT_SPEED * 
                                movementDirection * Time.deltaTime));
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

    public void SlowDown(float percent)
    {
        BASE_MOVEMENT_SPEED *= percent;
    }
}
