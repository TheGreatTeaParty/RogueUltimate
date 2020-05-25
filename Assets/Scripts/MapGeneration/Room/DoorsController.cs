using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    public LayerMask whatIsEnemy;
    public float width;

    public event EventHandler onDoorChanged;
    private bool is_sended = false;

    private void FixedUpdate()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, width, whatIsEnemy);
        if(enemies.Length > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = true;

                //Send info to change visualisation
                if (!is_sended)
                {
                    onDoorChanged?.Invoke(this, EventArgs.Empty);
                    is_sended = true;
                }
            }
            Minimap.instance.HideMap();
        }
        else
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = false;

                if (is_sended)
                {
                    onDoorChanged?.Invoke(this, EventArgs.Empty);
                    is_sended = false;
                }
            }
            Minimap.instance.ShowMap();
        }
    }
    private void OnDrawGizmosSelected() 
      {
        Gizmos.color= Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, width, 0));
      }
}
