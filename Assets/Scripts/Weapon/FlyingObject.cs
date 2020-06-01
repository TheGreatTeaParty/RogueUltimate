using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float speed;

    private int damage;
    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = KeepOnScene.instance.GetComponent<PlayerStat>().damage.GetValue();
        direction = InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection();
        rb.velocity = speed * direction;

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
            Destroy(this.gameObject);
    }
}
