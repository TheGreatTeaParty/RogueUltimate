using System;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    public LayerMask whatIsEnemy;
    public float width;
    public event EventHandler onDoorChanged;
    private bool _isSent = false;

    // Cache
    private BoxCollider2D[] _doorColliders;


    private void Start()
    {
        // Cache
        _doorColliders = new BoxCollider2D[doors.Length];
        for (int i = 0; i < doors.Length; i++)
            _doorColliders[i] = doors[i].GetComponent<BoxCollider2D>();
    }
    
    private void FixedUpdate()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, width, whatIsEnemy);
        if (enemies.Length > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                _doorColliders[i].enabled = true;

                //Send info to change visualisation
                if (!_isSent)
                {
                    onDoorChanged?.Invoke(this, EventArgs.Empty);
                    _isSent = true;
                }
            }
            Minimap.instance.HideMap();
        }
        else
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].GetComponent<BoxCollider2D>().enabled = false;

                if (_isSent)
                {
                    onDoorChanged?.Invoke(this, EventArgs.Empty);
                    _isSent = false;
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
