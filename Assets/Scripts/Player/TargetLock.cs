﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    private Collider2D target;
    private Collider2D _playerColider;
    private PlayerCamera playerCamera;
    private PlayerMovement playerMovement;
    private Vector3 dir;
    private float _waitTime = 0.8f;
    private float _currentTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = PlayerCamera.Instance;
        playerMovement = PlayerOnScene.Instance.playerMovement;
        _playerColider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target)
        {
            playerCamera.SetOffset((target.transform.position - _playerColider.bounds.center).normalized);
            dir = (target.bounds.center - _playerColider.bounds.center).normalized;
        }
        else
        {
            playerCamera.SetOffset(Vector2.zero);
        }

    }
    private void Update()
    {
        if(_currentTime > 0)
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
            if (collision.gameObject == target)
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
}
