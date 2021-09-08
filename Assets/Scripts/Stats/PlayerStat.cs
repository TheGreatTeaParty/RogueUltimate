using UnityEngine;
using System;
using Firebase.Analytics;


public class PlayerStat : CharacterStat, IDamaged
{
    private int _xp = 0;
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
        6220, // 14
        7400, // 15
        
    };
    private float _currentMana;
    private float _currentStamina;
    public int Kills = 0;
    private int _skillPoints = 0;
    private int _statPoints = 0;
    private InterfaceManager _interfaceManager;
 
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
    private bool _isDeathStarted = false;

    //Attributes:
    public StrengthAttribute Strength;
    public AgilityAttribute Agility;
    public IntelligenceAttribute Intelligence;

    public Stat AttackRange => attackRange;
    public Stat PushForce => pushForce;
    public Stat KnockBack => knockBack;
    public Stat AttackSpeed => attackSpeed;
    public Stat CastSpeed => castSpeed;

    public float CurrentMana
    {
        get => _currentMana;
        set => _currentMana = value;
    }
    public float CurrentStamina
    {
        get => _currentStamina;
        set => _currentStamina = value;
    }

    public int XP
    {
        get => _xp;
        set => _xp = value;
    }

    public int StatPoints
    {
        get => _statPoints;
        set => _statPoints = value;
    }
    public int SkillPoints
    {
        get => _skillPoints;
        set
        {
            _skillPoints = value;
            OnSkillPointGained?.Invoke();
        }
    }


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;

    public Action<float> OnHealthChanged;
    public Action<float> OnStaminaChanged;
    public Action<float> OnManaChanged;
    public Action<float> OnXPChanged;
    public Action OnEvadeTriggered;
    public Action OnKillChanged;
    public ITraitReqired EquipmentTraitReq;
    public Action OnSkillPointGained;

    public TraitHolder PlayerTraits;

    public ContractHolder PlayerContracts;

    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    
    
    private void Start()
    {
        if(PlayerTraits == null)
            PlayerTraits = new TraitHolder();

        if (PlayerContracts == null)
            PlayerContracts = new ContractHolder();

        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        _interfaceManager = InterfaceManager.Instance;
        level = 1;
    }
    public void SetUpPlayerInfo()
    {
        currentHealth = Strength.MaxHealth.Value;
        _currentStamina = Agility.MaxStamina.Value;
        _currentMana = Intelligence.MaxMana.Value;
    }
    protected override void Update()
    {
        //If we are disbled to move and Attack:
        if (!AllowControll)
        {
            playerMovement.StopMoving();
            playerAttack.AllowedToAttack = false;
        }
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
        if (_xp > _xpToNextLevel[level - 1])
        {
            _xp -= _xpToNextLevel[level - 1];
            LevelUp();
        }
        
        onChangeCallback?.Invoke();
        OnXPChanged?.Invoke(_xp);
    }
    
    private void LevelUp()
    {
        level++;
        _statPoints += 1;
        _interfaceManager.HighlightPanelButton?.Invoke(WindowType.Stats);

        if (level % 3 == 0)
        {
            ++_skillPoints;
            _interfaceManager.HighlightPanelButton?.Invoke(WindowType.SkillTree);
        }

        _interfaceManager.HighlightNavButton?.Invoke();
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
                        float healthPercent = currentHealth/ Strength.MaxHealth.Value;
                        Strength.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        currentHealth = Strength.MaxHealth.Value * healthPercent;
                        OnHealthChanged?.Invoke(currentHealth);
                        break;
                    }

                case StatType.Mind:
                    {
                        float ManaPercent = _currentMana / Intelligence.MaxMana.Value;
                        Intelligence.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        _currentMana = Intelligence.MaxMana.Value * ManaPercent;
                        OnManaChanged?.Invoke(_currentMana);
                        break;
                    }

                case StatType.Reaction:
                    {
                        float StaminaPercent =_currentStamina / Agility.MaxStamina.Value;
                        Agility.ModifyAttribute(new StatModifier(1, StatModifierType.Flat));
                        _currentStamina = Agility.MaxStamina.Value * StaminaPercent;
                        OnStaminaChanged?.Invoke(_currentStamina);
                        break;
                    }
            }

            onChangeCallback?.Invoke();
        }
    }
    public void AddAttributePoint(StatType statType,float value)
    {
            switch (statType)
            {
                case StatType.Will:
                    {
                        break;
                    }

                case StatType.Physique:
                    {
                        float healthPercent = currentHealth / Strength.MaxHealth.Value;
                        Strength.ModifyAttribute(new StatModifier(value, StatModifierType.Flat));
                        currentHealth = Strength.MaxHealth.Value * healthPercent;
                        OnHealthChanged?.Invoke(currentHealth);
                        break;
                    }

                case StatType.Mind:
                    {
                        float ManaPercent = _currentMana / Intelligence.MaxMana.Value;
                        Intelligence.ModifyAttribute(new StatModifier(value, StatModifierType.Flat));
                        _currentMana = Intelligence.MaxMana.Value * ManaPercent;
                        OnManaChanged?.Invoke(_currentMana);
                        break;
                    }

                case StatType.Reaction:
                    {
                        float StaminaPercent = _currentStamina / Agility.MaxStamina.Value;
                        Agility.ModifyAttribute(new StatModifier(value, StatModifierType.Flat));
                        _currentStamina = Agility.MaxStamina.Value * StaminaPercent;
                        OnStaminaChanged?.Invoke(_currentStamina);
                        break;
                    }
            }

            onChangeCallback?.Invoke();
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
        _timeLeft = TICK_TIME;

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
        _timeLeft = TICK_TIME;

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
        //Trigger Evade:

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
        if (effectType == EffectType.Fire)
            return (1 - Intelligence.FireResistance.Value) * intensity;
        else if(effectType == EffectType.Freeze)
            return (1 - Intelligence.FreezeResistance.Value) * intensity;
        else if(effectType == EffectType.Curse)
            return (1 - Intelligence.CurseResistance.Value) * intensity;
        else if (effectType == EffectType.Poison)
            return (1 - Strength.PoisonResistance.Value) * intensity;
        else if (effectType == EffectType.Bleed)
            return (1 - Strength.BleedResistance.Value) * intensity;
        else if(effectType == EffectType.Stun)
            return (1 - Strength.DazeResistance.Value) * intensity;
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
        else if (intensity < 0)
        {
            playerMovement.IncreaseMovementSpeed(-intensity);
        }
        else
        {
            playerMovement.DecreaseMovementSpeed(intensity);
        }
    }
    public override void Die()
    {
        if (!_isDeathStarted)
        {
            FirebaseAnalytics.LogEvent("player_death",
                new Parameter("level", level),
                new Parameter("int", Intelligence.GetBaseValue()),
                new Parameter("agil", Agility.GetBaseValue()),
                new Parameter("stren", Strength.GetBaseValue()),
                new Parameter("kills", Kills));

            _isDeathStarted = true;
            animator.SetTrigger("Die");
            gameObject.layer = 2;
            gameObject.tag = "Untagged";
            AccountManager.Instance.Renown += (Kills * 10);
            InterfaceManager.Instance.HideFaceElements();
            InterfaceManager.Instance.gameObject.GetComponentInChildren<DiePanel>().PlayerStartDie();
        }
    }

    private bool Dodge()
    {
        if(UnityEngine.Random.value > Agility.dodgeChance.Value/100f)
        {
            return false;
        }
        OnEvadeTriggered?.Invoke();
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

    public bool CheckRequirenments(EquipmentItem equipmentItem)
    {
        if (Agility.GetBaseValue() >= equipmentItem.GetRequiredAgility() &&
              Strength.GetBaseValue() >= equipmentItem.GetRequiredStrength() &&
                  Intelligence.GetBaseValue() >= equipmentItem.GetRequiredIntelligence())
        {
            if(EquipmentTraitReq!= null)
            {
                return EquipmentTraitReq.ISAppropriate(equipmentItem);
            }
            return true;
        }
        else
            return false;
    }

}
