using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    public Transform TargetCircle;

    private Collider2D target;
    private Collider2D _playerColider;
    private PlayerCamera playerCamera;
    private PlayerMovement playerMovement;
    private Vector3 dir;
    private float _waitTime = 0.8f;
    private float _currentTime = 0.8f;
    private GameObject current_circle;

    private Vector3 _offset;
    private Vector3 _basePos;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = PlayerCamera.Instance;
        playerMovement = PlayerOnScene.Instance.playerMovement;
        _playerColider = GetComponent<Collider2D>();

        _basePos = new Vector3(10000, 10000, 0);
        _offset = new Vector3(0, -0.45f, 0);
        current_circle = Instantiate(TargetCircle, _basePos, Quaternion.identity).gameObject;
    }
    public Vector3 GetEnemyDir()
    {
        return dir;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(target)
        {
            if(current_circle.transform.position!= target.transform.position + _offset)
                current_circle.transform.position = target.transform.position + _offset;

            playerCamera.SetOffset((target.transform.position - _playerColider.bounds.center).normalized);
            dir = (target.bounds.center - _playerColider.bounds.center).normalized;
        }
        else
        {
            if (current_circle.transform.position != _basePos)
                current_circle.transform.position = _basePos;
            playerCamera.SetOffset(Vector2.zero);
        }

    }
    private void Update()
    {
        if(_currentTime >= 0)
            _currentTime -= Time.deltaTime;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!target)
            {
                if (CheckForTheWay(collision.gameObject.transform.position))
                {
                    if (_currentTime <= 0)
                    {
                        target = collision;
                        playerMovement.SetLockMoving(true);
                        _currentTime = _waitTime;
                    }
                }
            }
            else if (collision.gameObject != target)
            {
                if ((collision.gameObject.transform.position - _playerColider.bounds.center).magnitude < (target.bounds.center - _playerColider.bounds.center).magnitude)
                {
                    if (CheckForTheWay(collision.gameObject.transform.position))
                    {
                        if (_currentTime <= 0)
                        {
                            target = collision;
                            playerMovement.SetLockMoving(true);
                            _currentTime = _waitTime;
                        }
                    }
                }
            }
            else
            {
                if (!CheckForTheWay(target.transform.position))
                {
                    target = null;
                    playerMovement.SetLockMoving(false);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision == target)
            {
                playerMovement.SetLockMoving(false);
                target = null;
            }
        }
    }

    private bool CheckForTheWay(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, position - transform.position, (position - transform.position).magnitude, LayerMask.GetMask("Wall"));
        if (hit)
            return false;
        return true;
    }
    public Vector3 GetDir()
    {
        return dir;
    }
    public Vector3 GetTargetPosition()
    {
        if (target)
            return target.transform.position;
        else
        {
            return Vector3.zero;
        }
    }
}
