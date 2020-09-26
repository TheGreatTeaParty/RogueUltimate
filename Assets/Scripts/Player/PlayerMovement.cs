using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float movementSpeed;
    public float BASE_MOVEMENT_SPEED;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator; 
    [SerializeField] protected Joystick joystick;
    private JoystickAttack _rangeJoystick;
    private Vector2 _movementDirection;
    private Vector2 _direction;
    private bool _stopped = false;
    private bool _rangeMoving = false;

    public void Start()
    {
        joystick = InterfaceManager.Instance.GetComponentInChildren<Joystick>();
    }

    /*There we receive input information*/
    void Update() 
    {
        ProcessInputs();

        //Save the direction of player movement
        if (!_rangeMoving && _movementDirection.x != 0 || _movementDirection.y != 0)
        {
            animator.SetFloat("Horizontal", _movementDirection.x);
            animator.SetFloat("Vertical", _movementDirection.y);
            _direction = _movementDirection;
        }
        else if(_rangeMoving)
        {
            animator.SetFloat("Horizontal", _rangeJoystick.GetDirection().x);
            animator.SetFloat("Vertical", _rangeJoystick.GetDirection().y);
            _direction = _rangeJoystick.GetDirection();
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
        _movementDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        movementSpeed = Mathf.Clamp(_movementDirection.magnitude, 0.0f, 1.0f);
        _movementDirection.Normalize();

    }
    
    /*This is the main function which calculates Character's movement*/
    void MoveCharacter()
    {
        if(!_stopped)
            rb2D.MovePosition((Vector2)transform.position + 
                            (movementSpeed * BASE_MOVEMENT_SPEED * 
                                _movementDirection * Time.deltaTime));
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
        return _direction;
    }

    public void SlowDown(float percent)
    {
        BASE_MOVEMENT_SPEED *= percent;
    }

    public void StopMoving()
    {
        _stopped = true;
    }

    public void StartMoving()
    {
        _stopped = false;
    }

    public void SetRangeMoving(bool state)
    {
        _rangeMoving = state;
    }
    public void SetRangeJoystick(JoystickAttack joystick)
    {
        _rangeJoystick = joystick;
    }
}
