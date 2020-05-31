using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;

    private int damage;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = KeepOnScene.instance.GetComponent<PlayerStat>().damage.GetValue();
        rb.velocity = speed * KeepOnScene.instance.GetComponent<PlayerMovment>().GetDirection();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<CharacterStat>().TakeDamage(damage);
            Destroy(this);
        }
        else if(collision.tag != "Player")
        {
            Destroy(this);
        }
    }
}
