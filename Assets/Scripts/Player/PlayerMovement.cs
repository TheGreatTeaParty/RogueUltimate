﻿using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float movementSpeed;
    private float BASE_MOVEMENT_SPEED = 3.5f;
    private float BASE_SPEED;
    public float ROLL_TIME = 0.4f;
    public Collider2D PlayerCollider;
    public TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator; 
    [SerializeField] protected Joystick joystick;
    private JoystickAttack _rangeJoystick;
    private TargetLock _targetLock;
    [SerializeField]
    private Vector2 _movementDirection;
    private Vector2 _direction;
    private bool _stopped = false;
    private bool _rangeMoving = false;

    private bool _isKeyboardAllowed = false;

    private bool _LockMovement = false;

    private PlayerStat _playerStat;
    private EquipmentAnimationHandler equipmentAnimation;

    private float _rollCurrentCD = 0;
    private float _rollCD = 1f;
    public bool isControlDisabled = false;

    private CharacterAudio characterAudio;


    public void Start()
    {
        BASE_SPEED = BASE_MOVEMENT_SPEED;
        characterAudio = GetComponent<CharacterAudio>();
        PlayerCollider = GetComponent<Collider2D>();
        equipmentAnimation = GetComponentInChildren<EquipmentAnimationHandler>();
        joystick = InterfaceManager.Instance.fixedJoystick;
        _playerStat = GetComponent<PlayerStat>();

        if (SettingsManager.instance.GetSetting(SettingsManager.SettingsKeys.isKeyboardAllowed) == "True")
        {
            _isKeyboardAllowed = true;
        }

        _targetLock = GetComponentInChildren<TargetLock>();

        if (SettingsManager.instance.GetSetting(SettingsManager.SettingsKeys.isKeyboardAllowed) == "true")
        {
            InterfaceManager.Instance.DisableView();
        }
    }

    /*There we receive input information*/
    void Update() 
    {
        ProcessInputs();
        if (_rollCurrentCD > 0) _rollCurrentCD -= Time.deltaTime;
    }
    

    /*I called func. moveCharacter there, because FixedUpdate is better for physic detection*/
    void FixedUpdate() 
    {
        //Save the direction of player movement
        if (!_rangeMoving && (_movementDirection.x != 0 || _movementDirection.y != 0) && !_stopped && !_LockMovement)
        {
            _direction = _movementDirection;
        }
        else if(_rangeMoving && !_stopped && _rangeJoystick.GetDirection().x!= 0 && _rangeJoystick.GetDirection().y != 0)
        {
            if(_rangeJoystick.GetDirection()!= Vector2.zero)
                _direction = _rangeJoystick.GetDirection();
        }

        else if(_LockMovement)
        {
            _direction = _targetLock.GetDir();
        }

        animator.SetFloat("Speed", movementSpeed);
        MoveCharacter();
        equipmentAnimation.RotateWeapon(_direction);
        RotateCharacter();
    }
    
    void ProcessInputs()
    {
        if (_isKeyboardAllowed)
        {
            _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!_stopped)
                movementSpeed = Mathf.Clamp(_movementDirection.magnitude, 0.0f, 1.0f);
            else
                movementSpeed = 0f;
        }
		else
        {
            if (!_stopped)
            {
                _movementDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
                movementSpeed = Mathf.Clamp(_movementDirection.magnitude, 0.0f, 1.0f);
                _movementDirection.Normalize();
            }
            else
                movementSpeed = 0f;
        }
    }
    void RotateCharacter()
    {
        if (_direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (_direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    /*This is the main function which calculates Character's movement*/
    void MoveCharacter()
    {
        if (!_stopped)
            rb2D.MovePosition((Vector2) transform.position +
                              _movementDirection * (movementSpeed * BASE_MOVEMENT_SPEED * Time.deltaTime));
    }

    public void Push(Vector2 push_direction)
    {
        rb2D.AddForce(push_direction, ForceMode2D.Impulse);
    }
    
    public void attackCharacter(){

        animator.SetTrigger("isAttack");

    }
    public Vector3 GetDirection()
    {
        return _direction;
    }

    public void DecreaseMovementSpeed(float percent)
    {
        BASE_MOVEMENT_SPEED *= (1 - percent);
    }
    public void IncreaseMovementSpeed(float percent)
    {
        BASE_MOVEMENT_SPEED += BASE_SPEED * percent;
    }
    public float GetCurentMovementSpeed()
    {
        return BASE_MOVEMENT_SPEED;
    }

    public void StopMoving()
    {
        isControlDisabled = true;
        _stopped = true;
    }

    public void StartMoving()
    {
        isControlDisabled = false;
        rb2D.velocity = new Vector2(0,0);
        _stopped = false;
    }

    public bool IsStopped()
    {
        return _stopped;
    }
    public IEnumerator DisablePlayerControll(float time)
    {
        StopMoving();
        yield return new WaitForSeconds(time);
        StartMoving();
    }
    public void SetRangeMoving(bool state)
    {
        _rangeMoving = state;
    }
    public void SetLockMoving(bool state)
    {
        _LockMovement = state;
    }
    public bool GetLockMoving()
    {
        return _LockMovement;
    }
    public TargetLock GetTargetLock()
    {
        return _targetLock;
    }
    public void SetRangeJoystick(JoystickAttack joystick)
    {
        _rangeJoystick = joystick;
    }

    public void PushToDirection(float PushPower)
    {
        rb2D.AddForce(_direction * (PushPower * 100));
    }

    public void Roll()
    {
        if (movementSpeed > 0 && _rollCurrentCD <= 0 && !_stopped)
        {
            _rollCurrentCD = _rollCD;
            if (_playerStat.ModifyStamina(-4))
            {
                animator.SetTrigger("Roll");
                rb2D.AddForce(_movementDirection * (rb2D.mass * 600));
                characterAudio.PlayExtra(0);

                StartCoroutine(DisablePlayerControll(ROLL_TIME));
            }
        }
    }
    public void ModifyBaseSpeed(float value)
    {
        BASE_MOVEMENT_SPEED += value;
    }
}
