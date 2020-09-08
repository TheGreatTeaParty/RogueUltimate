using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public float speed;
    [SerializeField]
    string HitName;
    private int _physicalDamage;
    private int _magicDamage;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = speed * _direction;

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, _direction);
    }
    public void SetData(int _physicalDamage, int _magicDamage, Vector2 _direction)
    {
        this._physicalDamage = _physicalDamage;
        this._magicDamage = _magicDamage;
        this._direction = _direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.Play(HitName);
        if (collision.GetComponent<IDamaged>()!= null)
           collision.GetComponent<IDamaged>().TakeDamage(_physicalDamage,_magicDamage);
        Destroy(gameObject);
    }
    
}
