using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float damage;
    public GameObject target;

    private void Start()
    {
        Destroy(gameObject, 0.8f);
    }

    private void FixedUpdate()
    {
        transform.position = target.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("EnvObjects"))
            {
                IDamaged damaged = collision.GetComponent<IDamaged>();
                if (damaged != null)
                {
                    damaged.TakeDamage(damage, 0);
                }
            }
        }
    }
}
