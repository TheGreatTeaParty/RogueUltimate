using System;
using UnityEngine;
using System.Collections;

public class EnemyStat : CharacterStat, IDamaged
{
    //[SerializeField] Transform coin;
    //[SerializeField] Transform XPOrb;

    [SerializeField] private int gainedXP;
    [SerializeField] private int gainedGold;
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
    private CharacterAudio _characterAudio;

    //Material
    private MaterialPropertyBlock _collideMaterial;
    private SpriteRenderer _materialInfo;

    private void Start()
    {
        _enemyAi = GetComponent<EnemyAI>();
        
        // Cache
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _floatingNumber = GetComponent<FloatingNumber>();
        _characterAudio = GetComponent<CharacterAudio>();

        maxHealth += level * 10;
        currentHealth = maxHealth;

        //Material:
        _collideMaterial = new MaterialPropertyBlock();
        _materialInfo = GetComponent<SpriteRenderer>();
        _materialInfo.GetPropertyBlock(_collideMaterial);
    }

   
    public override bool TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);

        //If we need to do armor or evades check character stats!

        onReceivedDamage?.Invoke(damageReceived);
        onDamaged?.Invoke();

        //Make enemy blinding
        StartCoroutine(WaitAndChangeProperty());
        if(_characterAudio)
            _characterAudio.DamageSound();
        return true;
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
        //
        onDie?.Invoke();
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.Sleep();
        _capsuleCollider2D.enabled = false;
        _floatingNumber.enabled = false;
        Destroy(this);
    }

    private IEnumerator WaitAndChangeProperty()
    {
        _collideMaterial.SetFloat("Damaged", 1f);
        _materialInfo.SetPropertyBlock(_collideMaterial);

        if (currentHealth <= 0)
        {
            _collideMaterial.SetFloat("Damaged", 0f);
            _materialInfo.SetPropertyBlock(_collideMaterial);
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(0.24f);

        _collideMaterial.SetFloat("Damaged", 0f);
        _materialInfo.SetPropertyBlock(_collideMaterial);
    }

}
