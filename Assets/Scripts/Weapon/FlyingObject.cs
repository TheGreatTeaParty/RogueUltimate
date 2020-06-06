using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float speed;

    private int ph_damage;
    private int mg_damage;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ph_damage = KeepOnScene.instance.GetComponent<PlayerStat>().ph_damage.GetValue();
        mg_damage = KeepOnScene.instance.GetComponent<PlayerStat>().mg_damage.GetValue();
        direction = InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection();
        rb.velocity = speed * direction;

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyStat>().TakeDamage(ph_damage,mg_damage);
            Destroy(this.gameObject);
        }

        else if (collision.tag != "Player")
            Destroy(this.gameObject);
    }
}
