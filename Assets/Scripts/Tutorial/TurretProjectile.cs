using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TurretProjectile : MonoBehaviour
{
    private int wallLayer;
    private int playerLayer;

    [SerializeField] private float speed = 1f;
    
    public float Damage { get; set; }

    private Vector3 direction;
    
    private void Awake()
    {
        wallLayer = LayerMask.NameToLayer("Wall");
        playerLayer = LayerMask.NameToLayer("Player");
        direction = new Vector3();
    }

    private void Update()
    {
        transform.position += transform.right * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name);
        if (other.gameObject.layer == wallLayer)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == playerLayer && PlayerOnScene.Instance.playerMovement.isControlDisabled == false)
        {
            DealDamage();
            Destroy(gameObject);
        }
    }

    private void DealDamage()
    {
        PlayerOnScene.Instance.stats.TakeDamage(10, 0);
    }
}
