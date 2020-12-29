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
    private Vector2 _movementDirection;
    private Vector2 _direction;
    private bool _stopped = false;
    private bool _rangeMoving = false;

    private PlayerStat _playerStat;
    private bool _isWaiting = false;
    private bool _readyForRoll = false;

    private float _timeLeft = 0f;
    private float _timeToWait = 0.6f;

    private float _rollCurrentCD = 0;
    private float _rollCD = 1f;
    private Vector3 _prevDragDir;

    public void Start()
    {
        joystick = InterfaceManager.Instance.fixedJoystick;
        _playerStat = GetComponent<PlayerStat>();
    }

    /*There we receive input information*/
    void Update() 
    {
        ProcessInputs();
        HandleRollingOptions();
    }
    

    /*I called func. moveCharacter there, because FixedUpdate is better for physic detection*/
    void FixedUpdate() 
    {
        //Save the direction of player movement
        if (!_rangeMoving && _movementDirection.x != 0 || _movementDirection.y != 0 && !_stopped)
        {
            animator.SetFloat("Horizontal", _movementDirection.x);
            animator.SetFloat("Vertical", _movementDirection.y);
            _direction = _movementDirection;
        }
        else if(_rangeMoving && !_stopped)
        {
            animator.SetFloat("Horizontal", _rangeJoystick.GetDirection().x);
            animator.SetFloat("Vertical", _rangeJoystick.GetDirection().y);
            if(_rangeJoystick.GetDirection()!= Vector2.zero)
                _direction = _rangeJoystick.GetDirection();
        }
        animator.SetFloat("Speed", movementSpeed);
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
        _stopped = true;
    }

    public void StartMoving()
    {
        rb2D.velocity = new Vector2(0,0);
        _stopped = false;
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
    public void SetRangeJoystick(JoystickAttack joystick)
    {
        _rangeJoystick = joystick;
    }

    public void PushToDirection(float PushPower)
    {
        rb2D.AddForce(PushPower * _direction*100);
    }

    private void Roll(Vector3 dir)
    {
        _rollCurrentCD = _rollCD;
        if (_playerStat.ModifyStamina(-15))
        {
            animator.SetTrigger("Roll");
            rb2D.AddForce(dir * rb2D.mass * 600);
            StartCoroutine(DisablePlayerControll(ROLL_TIME));
        }
    }
    private void HandleRollingOptions()
    {
        if (_timeLeft > 0)
            _timeLeft -= Time.deltaTime;
        else
        {
            _timeLeft = 0;
            _isWaiting = false;
        }

        if (_rollCurrentCD > 0)
            _rollCurrentCD -= Time.deltaTime;
        else
            _rollCurrentCD = 0;

        if (movementSpeed == 0)
            _readyForRoll = true;

        //Checking for the second drag to roll
        if (movementSpeed > 0 && !_isWaiting)
        {
            _isWaiting = true;
            _timeLeft = _timeToWait;
            _readyForRoll = false;
            _prevDragDir = _movementDirection;
        }

        else if (movementSpeed > 0 && _isWaiting
            && _readyForRoll && _rollCurrentCD == 0 &&
            !_stopped && CompareDirections())
            Roll(_movementDirection);

    }
    private bool CompareDirections()
    {
        if ((_movementDirection.x > 0 && _prevDragDir.x < 0) ||
            _movementDirection.x < 0 && _prevDragDir.x > 0)
            return false;
        if ((_movementDirection.y > 0 && _prevDragDir.y < 0) ||
            _movementDirection.y < 0 && _prevDragDir.y > 0)
            return false;
        return true;
    }
}
