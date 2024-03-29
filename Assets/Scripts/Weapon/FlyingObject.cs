﻿using UnityEngine;
using UnityEngine.Serialization;

public class FlyingObject : MonoBehaviour
{
    public float speed;
    [Space]
    public Transform HitEffect;
    public Transform ExplodeEffect;
    public AudioClip FlyingAudio;
    [SerializeField]
    private AudioClip _hitAudio;
    private AudioSource _audioSource;
    private float _physicalDamage;
    private float _magicDamage;
    private float _knockBack;
    private bool _crit;
    private bool _damageEnemy;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private SpriteRenderer _spriteRenderer;
    private Effect _effect;
    private Collider2D _collider;
    
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = speed * _direction;
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(FlyingAudio)
            _audioSource.PlayOneShot(FlyingAudio);

        //Change the rotation of the object according to the vector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, _direction);
    }
    
    public void SetData(float physicalDamage, float magicDamage, Vector2 direction, bool crit,float knockback = 0, Effect effect = null, bool damageEnemy = true)
    {
        _physicalDamage = physicalDamage;
        _magicDamage = magicDamage;
        _direction = direction;
        _knockBack = knockback;
        _crit = crit;
        _effect = effect;
        _damageEnemy = damageEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rb.Sleep();
        _collider.enabled = false;
        _spriteRenderer.sprite = null;

        _audioSource.PlayOneShot(_hitAudio);
        //Create Explode FX
        Instantiate(ExplodeEffect, transform.position, Quaternion.identity);

        if (collision.GetComponent<IDamaged>() != null)
        {
            if (!_damageEnemy)
                if (collision.CompareTag("Enemy"))
                    return;

            collision.GetComponent<IDamaged>().TakeDamage(_physicalDamage, _magicDamage, _crit);
            //Check if enemy takes damage
            if (HitEffect)
            {
                //Assign Effect:
                if (_effect)
                {
                    if (Random.value < _effect._chance)
                    {
                        CharacterStat character = collision.GetComponent<CharacterStat>();
                        if (character)
                            character.EffectController.AddEffect(Instantiate(_effect), character);
                    }
                }

                Transform Effect = Instantiate(HitEffect, collision.GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                Effect.GetComponent<SpriteRenderer>().sortingOrder = collision.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
            if (rigidbody)
                rigidbody.AddForce(_direction * 100 * _knockBack);
        }
        Destroy(gameObject, 0.5f);
    }
    
}
