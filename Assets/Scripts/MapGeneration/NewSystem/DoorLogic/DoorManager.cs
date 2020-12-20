using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public float width;
    public float height;
    public Vector3 position;

    DoorVisual[] _doors;
    private bool _isSent = false;
    private LayerMask whatToDetact;
    private bool _playerIn = false;

    void Start()
    {
        _doors = GetComponentsInChildren<DoorVisual>();
        whatToDetact = LayerMask.GetMask("Player");
    }

    private void FixedUpdate()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position + position, new Vector2(width, height), 0, whatToDetact);
       
        if (enemies.Length > 0)   
        {
            if (!_playerIn)
            {
                whatToDetact = LayerMask.GetMask("Enemy");
                _playerIn = true;
                return;
            }

            if (!_isSent)
            {
                Minimap.instance.HideMap();
                for (int i = 0; i < _doors.Length; i++)
                {
                    //Send info to change visualisation
                    _doors[i].Close();
                }
                _isSent = true;
            }
        }
        else
        {
            if (_isSent)
            {
                Minimap.instance.ShowMap();

                for (int i = 0; i < _doors.Length; i++)
                {
                        _doors[i].Restore();
                        _isSent = false;
                }
                Destroy(this);
            }
           
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + position, new Vector2(width, height));
    }
}
