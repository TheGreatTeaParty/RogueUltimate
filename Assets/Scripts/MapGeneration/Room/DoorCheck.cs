using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{

    public LayerMask whatIsEnemy;
    public float width;
    
    // Cache
    private SpawnPoint _spawnPoint;


    private void Start()
    {
        _spawnPoint = GetComponentInParent<SpawnPoint>();
    }

    private void FixedUpdate()
    {
        Collider2D[] door = Physics2D.OverlapCircleAll(transform.position, width, whatIsEnemy);
        if (door.Length > 0)
            _spawnPoint.has_door = true;
        else if (door.Length == 0)
            _spawnPoint.has_door = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, width, 0));
    }
    
}
