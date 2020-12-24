using UnityEngine;
using System;


public class PlayerStat : CharacterStat, IDamaged
{
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
    [SerializeField] private Stat attackSpeed;
    [SerializeField] private Stat blockStrength;
    [SerializeField] private Stat castSpeed;
    [SerializeField] private Stat dodgeChance;
    [SerializeField] private Stat critDamage;
    [SerializeField] private Stat critChance;
    [Space]
    [SerializeField] private float regenerationSpeed;
    [Space]
    // Change script for these two guys ?
    [SerializeField] private Animator animator;
    [SerializeField] private Transform LevelUpEffect;
    // Cache 
    private PlayerMovement _playerMovement;

    public Stat Physique => physique;
    public Stat Will => will;
    public Stat Mind => mind;
    public Stat Reaction => reaction;

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

    
    
    private void Start()
    {
        _regenerationCoolDown = 0;
        regenerationSpeed = 5;

        currentHealth = maxHealth;
        _currentStamina = maxStamina;
        _currentMana = maxMana;
        
        _xp = 0;
        level = 1;
        
        // Cache
        _playerMovement = GetComponent<PlayerMovement>();
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
    
    public void RegenerateStamina()
    {
        _regenerationCoolDown += Time.deltaTime * regenerationSpeed;
        if (_regenerationCoolDown > 1)
        {
            ModifyStamina(1);
            _regenerationCoolDown = 0;
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
