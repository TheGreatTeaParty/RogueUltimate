using UnityEngine;
using System;
using System.Collections;


public class PlayerStat : CharacterStat, IDamaged
{
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
    private int _statPoints = 6;
    private float _currentMana;
    private float _currentStamina;

 
    [Space]
    [SerializeField] private Stat attackRange;
    [SerializeField] private Stat pushForce;
    [SerializeField] private Stat knockBack;
    [SerializeField] private Stat attackSpeed;
    [SerializeField] private Stat blockStrength;
    [SerializeField] private Stat castSpeed;

    [SerializeField] public Stat HPRegeneration;

    [Space]

    // Change script for these two guys ?
    [SerializeField] private Animator animator;
    [SerializeField] private Transform LevelUpEffect;


    //Attributes:
    public StrengthAttribute Strength;
    public AgilityAttribute Agility;
    public IntelligenceAttribute Intelligence;

    public Stat AttackRange => attackRange;
    public Stat PushForce => pushForce;
    public Stat KnockBack => knockBack;
    public Stat AttackSpeed => attackSpeed;
    public Stat CastSpeed => castSpeed;

    public float CurrentMana => _currentMana;
    public float CurrentStamina => _currentStamina;
    public int XP => _xp;
    public int StatPoints => _statPoints;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;

    public Action<float> OnHealthChanged;
    public Action<float> OnStaminaChanged;
    public Action<float> OnManaChanged;
    public Action<float> OnXPChanged;

    private PlayerMovement playerMovement;
    
    
    private void Start()
    {
        currentHealth = Strength.MaxHealth.Value;
        _currentStamina = Agility.MaxStamina.Value;
        _currentMana = Intelligence.MaxMana.Value;
        playerMovement = GetComponent<PlayerMovement>();
        _xp = 0;
        level = 1;
        
    }
    
    protected override void Update()
    {
        if (_timeLeft <= 0)
        {
            //Health Regen
            ModifyHealth(HPRegeneration.Value);
            //Stamina Regen
            ModifyStamina(Agility.StaminaRegeniration.Value);
            //MP Regen
            ModifyMana(Intelligence.ManaRegeniration.Value);

            EffectController.Tick();
            _timeLeft = TICK_TIME;
        }
        _timeLeft -= Time.deltaTime;
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

        if (_statPoints - 1 >= 0)
        {
            _statPoints--;
            switch (statType)
            {
                case StatType.Will:
                    {
                        break;
                    }

                case StatType.Physique:
                    {
                        float healthPercent = Strength.MaxHealth.Value / currentHealth;
                        Strength.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        currentHealth = Strength.MaxHealth.Value * healthPercent;
                        OnHealthChanged?.Invoke(currentHealth);
                        break;
                    }

                case StatType.Mind:
                    {
                        float ManaPercent = Intelligence.MaxMana.Value / _currentMana;
                        Intelligence.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        _currentMana = Intelligence.MaxMana.Value * ManaPercent;
                        OnManaChanged?.Invoke(_currentMana);
                        break;
                    }

                case StatType.Reaction:
                    {
                        float StaminaPercent = Agility.MaxStamina.Value / _currentStamina;
                        Agility.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        _currentStamina = Agility.MaxStamina.Value * StaminaPercent;
                        OnStaminaChanged?.Invoke(_currentStamina);
                        break;
                    }
            }

            onChangeCallback.Invoke();
        }
    }


    public bool ModifyHealth(float value)
    {
        if (currentHealth + value < 0)
            return false;

        currentHealth += value;
        if (currentHealth > Strength.MaxHealth.Value)
            currentHealth = Strength.MaxHealth.Value;

        onChangeCallback?.Invoke();
        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
            Die();
        return true;
    }
    
    public bool ModifyStamina(float value)
    {
        if (_currentStamina + value < 0)
            return false;

        _currentStamina += value;
        if (_currentStamina > Agility.MaxStamina.Value)
            _currentStamina = Agility.MaxStamina.Value;

        onChangeCallback?.Invoke();
        OnStaminaChanged?.Invoke(_currentStamina);

        return true;
    }
    
    public bool ModifyMana(float value)
    {
        if (_currentMana + value < 0)
            return false;
        _currentMana += value;
        if (_currentMana > Intelligence.MaxMana.Value)
            _currentMana = Intelligence.MaxMana.Value;

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

    public int GetXPToNextLevel(int level)
    {
        return _xpToNextLevel[level - 1];
    }


    //RETURNS TRUE or FALSE to make a function of EVADES,BLOCK, etc.
    public override bool TakeDamage(float phyDamage, float magDamage)
    {
        //Check dodge here:

        if (!Dodge())
        {
            base.TakeDamage(phyDamage, magDamage);
            //TRUE 
            OnHealthChanged?.Invoke(currentHealth);
            animator.SetTrigger("Taking Dmg");
            //Take Damage -> Screen shake MAYBE it will be removed later!
            if(currentHealth > 0)
                ScreenShakeController.Instance.StartShake(0.17f, 1f);
            return true;
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        ModifyHealth(-damage);
        //TRUE 
        OnHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Taking Dmg");
        ScreenShakeController.Instance.StartShake(0.17f, 1f);
    }

    //***                                   -----------  Effects: ----------
    public override float GetEffectResult(float intensity, EffectType effectType)
    {
        if (effectType == EffectType.Fire || effectType == EffectType.Freeze|| effectType == EffectType.Curse)
        {
            return (1 - Intelligence.ElementalEffectResistance.Value) * intensity;
        }
        else if (effectType == EffectType.Poison || effectType == EffectType.Bleed || effectType == EffectType.Stun)
        {
            return (1 - Strength.PhysicalEffectResistance.Value) * intensity;
        }
        else
        {
            return intensity;
        }
    }

    public override void TakeEffectDamage(float intensity)
    {
        ModifyHealth(-intensity);
        //TRUE 
        OnHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Taking Dmg");
        ScreenShakeController.Instance.StartShake(0.17f, 1f);
    }
    public override void ModifyMovementSpeed(float intensity)
    {
       if(intensity == 1)
        {
            playerMovement.StopMoving();
        }
       else if(intensity == 0)
        {
            playerMovement.StopMoving();
        }
        else
        {
            playerMovement.SlowDown(intensity);
        }
    }
    public override void Die()
    {
        animator.SetTrigger("Die");
        gameObject.layer = 2;
        gameObject.tag = "Untagged";
        InterfaceManager.Instance.HideFaceElements();
        InterfaceManager.Instance.gameObject.GetComponentInChildren<DiePanel>().PlayerStartDie();
    }

    private bool Dodge()
    {
        if(UnityEngine.Random.value > Agility.dodgeChance.Value/100f)
        {
            return false;
        }
        return true;
    }

    public (float, bool) GetPhysicalCritDamage()
    {
        if (UnityEngine.Random.value > Agility.CritChance.Value / 100f)
        {
            return (PhysicalDamage.Value,false);
        }
        else
        {
            return (PhysicalDamage.Value * (1+Strength.CritDamage.Value),true);
        }
    }

    public (float,bool) GetMagicalCritDamage()
    {
        if (UnityEngine.Random.value > Agility.CritChance.Value / 100f)
        {
            return (PhysicalDamage.Value,false);
        }
        else
        {
            return (PhysicalDamage.Value * (1 + Intelligence.MagicalCrit.Value),true);
        }
    }

    public bool TakeDamage(float phyDamage, float magDamage, bool crit)
    {
        return TakeDamage(phyDamage, magDamage);
    }
}
