using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat,IDamaged
{
    [Space] public int gainedXP;

    public delegate void OnReceivedDamage(int damage);
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
        
        
        int modifier = level * 10;

        maxHealth += level * 10;
        currentHealth = maxHealth;
        
        physicalDamage.AddModifier(modifier);
        magicDamage.AddModifier(modifier);
        physicalProtection.AddModifier(modifier);
        magicProtection.AddModifier(modifier);
    }
    
    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
     
        onReceivedDamage?.Invoke(magicDamageReceived+physicalDamageReceived);
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
