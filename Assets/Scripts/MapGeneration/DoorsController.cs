using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    public LayerMask whatIsEnemy;
    public float width;


    private void FixedUpdate()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, width, whatIsEnemy);
        if(enemies.Length > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    private void OnDrawGizmosSelected() 
      {
        Gizmos.color= Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, width, 0));
      }
}
