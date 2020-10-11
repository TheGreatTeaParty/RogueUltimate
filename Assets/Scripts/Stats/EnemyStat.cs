using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat, IDamaged
{
    [Space] public int gainedXP;

    public delegate void OnReceivedDamage(float damage);
    public OnReceivedDamage onReceivedDamage;

    public delegate void OnDamaged();
    public OnDamaged onDamaged;

    public delegate void OnDie();
    public OnDie onDie;
    
    // Cache
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private FloatingNumber _floatingNumber;

    
    public void Start()
    {
        // Cache
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _floatingNumber = GetComponent<FloatingNumber>();

        maxHealth += level * 10;
        currentHealth = maxHealth;
    }
    
    public override void TakeDamage(float physicalDamage, float magicDamage)
    {
        base.TakeDamage(physicalDamage, magicDamage);
     
        onReceivedDamage?.Invoke(damageReceived);
        _rigidbody2D.velocity = Vector2.zero;
        onDamaged?.Invoke();
    }
    
    public override void Die()
    {
        PlayerStat.Instance.GainXP(gainedXP);
        onDie?.Invoke();
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.Sleep();
        _capsuleCollider2D.enabled = false;
        _floatingNumber.enabled = false;
        Destroy(this);
    }
    
}
