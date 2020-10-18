using UnityEngine;
using UnityEngine.Serialization;

public class FlyingObject : MonoBehaviour
{
    public float speed;
    [FormerlySerializedAs("HitName")] [SerializeField]
    private string _hitName;
    private float _physicalDamage;
    private float _magicDamage;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = speed * _direction;

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, _direction);
    }
    
    public void SetData(float physicalDamage, float magicDamage, Vector2 direction)
    {
        _physicalDamage = physicalDamage;
        _magicDamage = magicDamage;
        _direction = direction;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.Play(_hitName);
        if (collision.GetComponent<IDamaged>()!= null)
           collision.GetComponent<IDamaged>().TakeDamage(_physicalDamage, _magicDamage);
        Destroy(gameObject);
    }
    
}
