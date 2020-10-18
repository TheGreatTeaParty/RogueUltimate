using System;
using UnityEngine;

public class EnemyStat : CharacterStat, IDamaged
{
    [SerializeField] private int gainedXP;
    private EnemyAI _enemyAi;
    
    
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

    
    private void Start()
    {
        _enemyAi = GetComponent<EnemyAI>();
        
        // Cache
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _floatingNumber = GetComponent<FloatingNumber>();

        maxHealth += level * 10;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    public override void TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);
     
        onReceivedDamage?.Invoke(damageReceived);
        _rigidbody2D.velocity = Vector2.zero;
        onDamaged?.Invoke();
    }

    public void TakeDamage(float receivedPhyDmg, float receivedMagDmg, Vector2 bounceDirection, float power)
    {
        base.TakeDamage(receivedPhyDmg, receivedMagDmg);
        
        onReceivedDamage?.Invoke(damageReceived);
        _rigidbody2D.AddForce(bounceDirection * power);
        onDamaged?.Invoke();
    }

    public override void Die()
    {
        CharacterManager.Instance.Stats.GainXP(gainedXP);
        onDie?.Invoke();
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.Sleep();
        _capsuleCollider2D.enabled = false;
        _floatingNumber.enabled = false;
        Destroy(this);
    }
    
}
