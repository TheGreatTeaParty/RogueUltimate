using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float speed;

    private int _physicalDamage;
    private int _magicDamage;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _physicalDamage = KeepOnScene.instance.GetComponent<PlayerStat>().physicalDamage.GetValue();
        _magicDamage = KeepOnScene.instance.GetComponent<PlayerStat>().magicDamage.GetValue();
        _direction = InterfaceOnScene.instance.GetComponentInChildren<JoystickAttack>().GetDirection();
        _rb.velocity = speed * _direction;

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, _direction);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStat>().TakeDamage(_physicalDamage,_magicDamage);
            Destroy(this.gameObject);
        }

        else if (!collision.CompareTag("Player"))
            Destroy(this.gameObject);
    }
}
