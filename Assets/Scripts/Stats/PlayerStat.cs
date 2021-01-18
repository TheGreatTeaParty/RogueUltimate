using UnityEngine;
using System;


public class PlayerStat : CharacterStat, IDamaged
{
    private float STAMINA_REGINERATION_DEALY = 2;
    private float _regenerationCoolDown;
    private int _xp;
    private int[] _xpToNextLevel = 
    {
        // multiplied by 3
        220, // 1
        440, // 2
        700, // 3
        980, // 4
        1300, // 5 
        1640, // 6
        2020, // 7
        2460, // 8
        2920, // 9
        3440, // 10
        4000, // 11
        4640, // 12
        5340, // 13
        
    };
    private int _statPoints = 0;
    private float _currentMana;
    private float _currentStamina;

    [SerializeField] private float maxMana;
    [SerializeField] private float maxStamina;
    [SerializeField] private float maxWill;
    [Space] 
    [SerializeField] private Stat physique;
    [SerializeField] private Stat will;
    [SerializeField] private Stat mind;
    [SerializeField] private Stat reaction;
    [Space]
    [SerializeField] private Stat attackRange;
    [SerializeField] private Stat pushForce;
    [SerializeField] private Stat knockBack;
    [SerializeField] private Stat attackSpeed;
    [SerializeField] private Stat blockStrength;
    [SerializeField] private Stat castSpeed;
    [SerializeField] private Stat dodgeChance;
    [SerializeField] private Stat critDamage;
    [SerializeField] private Stat critChance;
    [Space]
    [SerializeField] private float regenerationSpeed;  //Make it stat?
    [Space]
    // Change script for these two guys ?
    [SerializeField] private Animator animator;
    [SerializeField] private Transform LevelUpEffect;

    public Stat Physique => physique;
    public Stat Will => will;
    public Stat Mind => mind;
    public Stat Reaction => reaction;

    public Stat AttackRange => attackRange;
    public Stat PushForce => pushForce;
    public Stat KnockBack => knockBack;
    public Stat AttackSpeed => attackSpeed;
    public Stat CastSpeed => castSpeed;

    public float CurrentMana => _currentMana;
    public float CurrentStamina => _currentStamina;
    public float MaxMana => maxMana;
    public float MaxStamina => maxStamina;
    public float MaxWill => maxWill;
    public int XP => _xp;
    public int StatPoints => _statPoints;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;

    public Action<float> OnHealthChanged;
    public Action<float> OnStaminaChanged;
    public Action<float> OnManaChanged;
    public Action<float> OnXPChanged;

    
    
    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 2;

        currentHealth = maxHealth;
        _currentStamina = maxStamina;
        _currentMana = maxMana;
        
        _xp = 0;
        level = 1;
        
    }
    
    protected override void Update()
    {
        base.Update();
        RegenerateStamina();
    }

    public void GainXP(int gainedXP)
    {
        if (level >= 20) return; // max level
        
        _xp += gainedXP;
        while (_xp > _xpToNextLevel[level - 1]) 
        {
            _xp -= _xpToNextLevel[level - 1];
            LevelUp();
        }
        
        onChangeCallback.Invoke();
        OnXPChanged?.Invoke(_xp);
    }
    
    private void LevelUp()
    {
        level++;
        _statPoints += 1;

        //Sound + LevelUpFX
        AudioManager.Instance.Play("LevelUp");
        PlayerOnScene.Instance.playerFX.SpawnEffect(LevelUpEffect);
    }
    
    public void AddAttributePoint(StatType statType)
    {
        switch (statType)
        {
            case StatType.Will:
            {
                will.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
         
            case StatType.Physique:
            {
                physique.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
         
            case StatType.Mind:
            {
                mind.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
         
            case StatType.Reaction:
            {
                reaction.AddModifier(new StatModifier(1, StatModifierType.Flat));
                break;
            }
        }

        onChangeCallback.Invoke();
    }

    public void AddAttributePoint(StatType statType, int value)
    {
        for (int i = 0; i < value; i++)
            AddAttributePoint(statType);
    }

    protected override void TakeEffectDamage(Effect effect)
    {
        switch (effect.EffectType)
        {
            case EffectType.Burning:
                
                break;
            
            case EffectType.Freezing:

                break;
        }
    }

    public bool ModifyHealth(float value)
    {
        if (currentHealth + value < 0)
            return false;

        currentHealth += value;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        onChangeCallback?.Invoke();
        return true;
    }
    
    public bool ModifyStamina(float value)
    {
        if (_currentStamina + value < 0)
            return false;

        _currentStamina += value;
        if (_currentStamina > maxStamina)
            _currentStamina = maxStamina;

        onChangeCallback?.Invoke();
        OnStaminaChanged?.Invoke(_currentStamina);

        //Set timer to stamina regineration:
        if(value < 0)
            _regenerationCoolDown = 0;
        return true;
    }
    
    public bool ModifyMana(float value)
    {
        if (_currentMana + value < 0)
            return false;
        _currentMana += value;
        if (_currentMana > maxMana)
            _currentMana = maxMana;

        onChangeCallback?.Invoke();
        OnManaChanged?.Invoke(_currentMana);
        return true;
    }
    public bool CheckHealth(float value)
    {
        if (currentHealth + value < 0)
            return false;
        return true;
    }
    public bool CheckStamina(float value)
    {
        if (_currentStamina + value < 0)
            return false;
        return true;
    }
    public bool CheckMana(float value)
    {
        if (_currentMana + value < 0)
            return false;
        return true;
    }
    public void RegenerateStamina()
    {
        if(_regenerationCoolDown <= STAMINA_REGINERATION_DEALY)
            _regenerationCoolDown += Time.deltaTime;
        else if (_regenerationCoolDown > STAMINA_REGINERATION_DEALY)
        {
            ModifyStamina(regenerationSpeed);
        }
    }

    public int GetXPToNextLevel(int level)
    {
        return _xpToNextLevel[level - 1];
    }

    public override void TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);
        OnHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Taking Dmg");
        //Take Damage -> Screen shake MAYBE it will be removed later!
        ScreenShakeController.Instance.StartShake(0.17f, 1f);
    }

    public override void Die()
    {
        animator.SetTrigger("Die");
        gameObject.layer = 2;
        gameObject.tag = "Untagged";
        InterfaceManager.Instance.HideFaceElements();
        InterfaceManager.Instance.gameObject.GetComponentInChildren<DiePanel>().PlayerDie(); //Opens Window with a decision |Adverb to continue| or |Humility|
        //transform.position = new Vector2(100, 100);
        //Destroy or set Active(false) 


        //Destroy(gameObject);
        //Destroy(InterfaceManager.Instance.gameObject);
        //SceneManager.LoadScene("Menu");
    }
    
}
