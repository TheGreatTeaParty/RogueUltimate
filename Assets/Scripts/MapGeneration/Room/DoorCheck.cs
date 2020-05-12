using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{

    public LayerMask whatIsEnemy;
    public float width;

    private void FixedUpdate()
    {
        Collider2D[] door = Physics2D.OverlapCircleAll(transform.position, width, whatIsEnemy);
        if(door.Length > 0)
        {
            GetComponentInParent<SpawnPoint>().has_door = true;
        }
        else if(door.Length == 0)
        {
            GetComponentInParent<SpawnPoint>().has_door = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, width, 0));
    }
}
