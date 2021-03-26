using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float movementSpeed;
    public float BASE_MOVEMENT_SPEED;
    public float ROLL_TIME = 0.4f;
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

    private float _rollCurrentCD = 0;
    private float _rollCD = 1f;
    public bool isConrolDisabled = false;

    private CharacterAudio characterAudio;


    public void Start()
    {
        characterAudio = GetComponent<CharacterAudio>();
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
        if (!_rangeMoving && (_movementDirection.x != 0 || _movementDirection.y != 0) && !_stopped)
        {
            animator.SetFloat("Horizontal", _movementDirection.x);
            animator.SetFloat("Vertical", _movementDirection.y);
            _direction = _movementDirection;
        }
        else if(_rangeMoving && !_stopped && _rangeJoystick.GetDirection().x!= 0 && _rangeJoystick.GetDirection().y != 0)
        {
            animator.SetFloat("Horizontal", _rangeJoystick.GetDirection().x);
            animator.SetFloat("Vertical", _rangeJoystick.GetDirection().y);
            if(_rangeJoystick.GetDirection()!= Vector2.zero)
                _direction = _rangeJoystick.GetDirection();
        }

        else if(!_stopped && _LockMovement)
        {
            animator.SetFloat("Horizontal", _targetLock.GetDir().x);
            animator.SetFloat("Vertical", _targetLock.GetDir().y);
            _direction = _targetLock.GetDir();
        }

        animator.SetFloat("Speed", movementSpeed);
        MoveCharacter();
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

    public void SlowDown(float percent)
    {
        BASE_MOVEMENT_SPEED *= percent;
    }

    public void StopMoving()
    {
        isConrolDisabled = true;
        _stopped = true;
    }

    public void StartMoving()
    {
        isConrolDisabled = false;
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
}
