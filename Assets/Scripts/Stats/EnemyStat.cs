using System;
using UnityEngine;
using System.Collections;

public class EnemyStat : CharacterStat, IDamaged
{
    [Space]
    [SerializeField] Transform coin;
    [SerializeField] Transform XPOrb;
    [SerializeField] private int gainedXP;
    [SerializeField] private int gainedGold;
    private EnemyAI _enemyAi;
    
    
    public delegate void OnReceivedDamage(float damage,bool _isCrit);
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

    private bool _hasChanged = false;
    private void Start()
    {
        _enemyAi = GetComponent<EnemyAI>();
        
        // Cache
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _floatingNumber = GetComponent<FloatingNumber>();
        _characterAudio = GetComponent<CharacterAudio>();

        currentHealth = maxHealth;

        //Material:
        _collideMaterial = new MaterialPropertyBlock();
        _materialInfo = GetComponent<SpriteRenderer>();
        _materialInfo.GetPropertyBlock(_collideMaterial);
    }
    protected override void Update()
    {
        base.Update();
        if (!AllowControll)
        {
            if (_enemyAi)
            {
                _hasChanged = true;
                _enemyAi.StopMoving();
                _enemyAi.DisableControll();
            }
        }
        else
        {
            if (_enemyAi && _hasChanged)
            {
                _enemyAi.StartMoving();
                _enemyAi.EnableControll();
                _hasChanged = false;
            }
        }
    }

    public override bool TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);

        //If we need to do armor or evades check character stats!

        onReceivedDamage?.Invoke(damageReceived,false);
        onDamaged?.Invoke();

        //Make enemy blinding
        StartCoroutine(WaitAndChangeProperty());
        if(_characterAudio)
            _characterAudio.DamageSound();
        return true;
    }

    public bool TakeDamage(float phyDamage, float magDamage,bool _isCrit)
    {
        base.TakeDamage(phyDamage, magDamage);

        //If we need to do armor or evades check character stats!

        onReceivedDamage?.Invoke(damageReceived, _isCrit);
        onDamaged?.Invoke();

        //Make enemy blinding
        StartCoroutine(WaitAndChangeProperty());
        if (_characterAudio)
            _characterAudio.DamageSound();
        return true;
    }

    public void TakeDamage(float receivedPhyDmg, float receivedMagDmg, Vector2 bounceDirection, float power)
    {
        base.TakeDamage(receivedPhyDmg, receivedMagDmg);
        
        onReceivedDamage?.Invoke(damageReceived,false);
        _rigidbody2D.AddForce(bounceDirection * power);
        onDamaged?.Invoke();
    }
    public void TakeDamage(float damage)
    {
        //If we need to do armor or evades check character stats!
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();

        onReceivedDamage?.Invoke(damage, false);
        onDamaged?.Invoke();

        //Make enemy blinding
        StartCoroutine(WaitAndChangeProperty());
        if (_characterAudio)
            _characterAudio.DamageSound();
    }

    public override float GetEffectResult(float intensity, EffectType effectType)
    {
        return intensity;
    }
    public override void TakeEffectDamage(float intensity)
    {
        TakeDamage(intensity);
    }
    public override void ModifyMovementSpeed(float intensity)
    {
       if(intensity == 1)
        {
            _enemyAi.StopMoving();
        }

       else if (intensity == 0)
        {
            _enemyAi.StartMoving();
        }

        else if(intensity < 0)
        {
            _enemyAi.IncreaseMovementSpeed(-intensity);
        }
        else
        {
            _enemyAi.DecreaseMovementSpeed(intensity);
        }
    }

    public override void Die()
    {
        //Remove all effects:
        EffectController.RemoveAll();

        PlayerStat playerStat = CharacterManager.Instance.Stats;
        playerStat.Kills++;
        playerStat.OnKillChanged?.Invoke();

        GenerateDrop();

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

    public virtual void SetLevel(int level)
    {
        this.level = level;
    }

    protected void GenerateDrop()
    {
        //Spawn Gold and XP
        Transform gold = Instantiate(coin, transform.position, Quaternion.identity);
        gold.GetComponent<Gold>().GoldAmount = gainedGold;

        Transform Xp = Instantiate(XPOrb, transform.position, Quaternion.identity);
        Xp.GetComponent<XP>().XPAmount = gainedXP;

        if (UnityEngine.Random.value < 0.25f)
        {
            ItemScene.SpawnItemScene(transform.position, ItemsAsset.instance.GenerateItemBasedLevel(CharacterManager.Instance.Stats.Level));
        }
    }

    protected override void Stager()
    {
        _enemyAi.StopEnemyAttack();
        onDamaged?.Invoke();
    }
}
