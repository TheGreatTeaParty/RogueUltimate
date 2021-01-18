using UnityEngine;
using UnityEngine.Serialization;

public class FlyingObject : MonoBehaviour
{
    public float speed;
    [Space]
    public Transform HitEffect;
    [FormerlySerializedAs("HitName")] [SerializeField]
    private string _hitName;
    private float _physicalDamage;
    private float _magicDamage;
    private float _knockBack;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = speed * _direction;

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, _direction);
    }
    
    public void SetData(float physicalDamage, float magicDamage, Vector2 direction,float knockback = 0)
    {
        _physicalDamage = physicalDamage;
        _magicDamage = magicDamage;
        _direction = direction;
        _knockBack = knockback;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.Play(_hitName);
        if (collision.GetComponent<IDamaged>() != null)
        {
            collision.GetComponent<IDamaged>().TakeDamage(_physicalDamage, _magicDamage);
            //Check if enemy takes damage
            if (HitEffect)
            {
                Transform Effect = Instantiate(HitEffect, collision.GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                Effect.GetComponent<SpriteRenderer>().sortingOrder = collision.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if(rigidbody)
                rigidbody.AddForce(_direction * 100 * _knockBack);
        }
        Destroy(gameObject);
    }
    
}
