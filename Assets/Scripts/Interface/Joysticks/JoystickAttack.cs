using UnityEngine;

public class JoystickAttack : MonoBehaviour
{
    public Transform AimArrow;
    protected Joystick joystick;

    private Vector2 _movement;
    private bool _isSlowed;
    
    // Cache
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private AimDisplay _aimDisplay;
    private EquipmentAnimationHandler equipmentAnimation;
    
    
    public void Start()
    {
        _isSlowed = false;
        joystick = GetComponent<Joystick>();
        
        // Cache
        _playerMovement = PlayerOnScene.Instance.playerMovement;
        _playerAttack = PlayerOnScene.Instance.playerAttack;
        _aimDisplay = _playerAttack.GetComponentInChildren<AimDisplay>();
        equipmentAnimation = PlayerOnScene.Instance.equipmentAnimationHandler;
    }

    /*There we receive input information*/
    private void Update()
    {
        _movement = joystick.Direction;
        if (_movement != Vector2.zero)
        {
            equipmentAnimation.RotateRangeWeapon(_movement.normalized);

            if (_aimDisplay.GetIconState())
                RotateAimArrow(_movement);
        }
    }

    private void FixedUpdate()
    {
        if (_movement.x != 0 || _movement.y != 0)
        {
            //if not slowed, slow the character down
            if (!_isSlowed)
            {
                _playerMovement.SetRangeJoystick(this);
                _playerMovement.DecreaseMovementSpeed(0.5f);
                _playerMovement.SetRangeMoving(true);
                _isSlowed = true;
            }
            DisplayAimArrow();
            _playerAttack.Attack();

        }

        //Return the normal speed;
        else if (_isSlowed)
        {
            DisableAimArrow();
            _playerAttack.StopAttack();
            _playerMovement.IncreaseMovementSpeed(0.5f);
            _playerMovement.SetRangeMoving(false);
            _isSlowed = false;

            equipmentAnimation.RotateRangeWeapon(new Vector3(0f,0f,0f));

            if (_aimDisplay.GetIconState())
                RotateAimArrow(new Vector3(0f, 0f, 0f));
        }
    }

    public Vector2 GetDirection()
    {
        return _movement.normalized;
    }

    private void DisplayAimArrow()
    {
        _aimDisplay.TurnOnIcon();
    }

    private void DisableAimArrow()
    {
        _aimDisplay.TurnOFFIcon();
    }

    private void RotateAimArrow(Vector3 dir)
    {
        _aimDisplay.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }
}
