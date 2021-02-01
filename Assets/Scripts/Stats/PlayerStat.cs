using UnityEngine;
using System;
using System.Collections;


public class PlayerStat : CharacterStat, IDamaged
{
    private float STAMINA_REGINERATION_DEALY = 2;
    private float _regenerationCoolDown;
    private float _manaRegenCoolDown;
    private float _healthRegenCoolDown;

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

    
    
    private void Start()
    {
        _regenerationCoolDown = 0;
        _healthRegenCoolDown = 0;
        _manaRegenCoolDown = 0;

        currentHealth = Strength.MaxHealth.Value;
        _currentStamina = Agility.MaxStamina.Value;
        _currentMana = Intelligence.MaxMana.Value;
        
        _xp = 0;
        level = 1;
        
    }
    
    protected override void Update()
    {
        base.Update();
        RegenerateStamina();
        RegenerateMana();
        //RegenerateHealth();
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
                        Strength.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        break;
                    }

                case StatType.Mind:
                    {
                        Intelligence.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        break;
                    }

                case StatType.Reaction:
                    {
                        Agility.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        break;
                    }
            }

            onChangeCallback.Invoke();
        }
    }
    /*
    public void AddAttributePoint(StatType statType, int value)
    {
        for (int i = 0; i < value; i++)
            AddAttributePoint(statType);
    }*/

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
        if (currentHealth > Strength.MaxHealth.Value)
            currentHealth = Strength.MaxHealth.Value;

        onChangeCallback?.Invoke();
        OnHealthChanged?.Invoke(currentHealth);
        if (value < 0)
            _healthRegenCoolDown = 0;
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
        if (_currentMana > Intelligence.MaxMana.Value)
            _currentMana = Intelligence.MaxMana.Value;

        onChangeCallback?.Invoke();
        OnManaChanged?.Invoke(_currentMana);
        if (value < 0)
            _manaRegenCoolDown = 0;
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
            ModifyStamina(Agility.StaminaRegeniration.Value / 10f);
        }
    }

    public void RegenerateMana()
    {
        if (_manaRegenCoolDown <= STAMINA_REGINERATION_DEALY)
            _manaRegenCoolDown += Time.deltaTime;
        else if (_manaRegenCoolDown > STAMINA_REGINERATION_DEALY / 10f)
        {
            ModifyMana(Intelligence.ManaRegeniration.Value);
        }
    }

    public void RegenerateHealth()
    {
        if (_healthRegenCoolDown <= STAMINA_REGINERATION_DEALY)
            _healthRegenCoolDown += Time.deltaTime;
        else if (_healthRegenCoolDown > STAMINA_REGINERATION_DEALY / 10f)
        {
            ModifyHealth(Strength.HPRegeneration.Value);
        }
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
            ScreenShakeController.Instance.StartShake(0.17f, 1f);
            return true;
        }
        return false;
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

    private bool Dodge()
    {
        if(UnityEngine.Random.value > Agility.dodgeChance.Value/100f)
        {
            return false;
        }
        return true;
    }
}
