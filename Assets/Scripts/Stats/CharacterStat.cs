using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStat : MonoBehaviour
{
    [FormerlySerializedAs("Name")] 
    [SerializeField] protected string characterName;
    protected int level;
    [Space] 
    [SerializeField] protected Stat physicalDamage;
    [SerializeField] protected Stat physicalProtection;
    [SerializeField] protected Stat magicProtection;
    [SerializeField] protected Stat magicDamage;
    [Space]
    [SerializeField] protected float maxHealth = 100;
    public float currentHealth;
    protected float damageReceived;
    public bool AllowControll = true;

    // Effects 
    public EffectController EffectController;
    protected float _timeLeft;
    protected const float TICK_TIME = 1f;

    //Stager
    private const float STAGER_TIME = 1f;
    protected float _stager_time_left;
    protected float _stagerDamage;
    protected float _stagerPercent = 0.25f;


    public string CharacterName => characterName;

    public int Level
    {
        get => level;
        set => level = value;
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public Stat PhysicalDamage => physicalDamage;
    public Stat PhysicalProtection => physicalProtection;
    public Stat MagicDamage => magicDamage;
    public Stat MagicProtection => magicProtection;


    // This is base class for all NPS, Player, Enemy so on
    // all phyDamage counting, phyDamage intake should be written here
    private void Awake()
    {
        EffectController = new EffectController();
        currentHealth = maxHealth;
        _timeLeft = TICK_TIME;
        _stagerDamage = 0;
    }

    protected virtual void Update()
    {
        if(_timeLeft <= 0)
        {
            EffectController.Tick();
            _timeLeft = TICK_TIME;
        }

        _timeLeft -= Time.deltaTime;

        if (_stager_time_left > 0)
            _stager_time_left -= Time.deltaTime;


    }

    public virtual float GetEffectResult(float intensity, EffectType effectType)
    {
        return 0;
    }

    public virtual bool IsEffectApplied(float chance, EffectType effectType)
    {
        return true;
    }

    public virtual void ModifyMovementSpeed(float intensity)
    {
        
    }

    public virtual void TakeEffectDamage(float intensity)
    {

    }



    public virtual bool TakeDamage(float _phyDamage, float _magDamage)
    {
        //Calculate the incoming damage:
        float phyDamage = _phyDamage - physicalProtection.Value;
        float magDamage = _magDamage - magicProtection.Value;
        damageReceived = (phyDamage + magDamage);

        //Trigger the Stager Timer:
        _stager_time_left = STAGER_TIME;
        StagerLogic(damageReceived);

        currentHealth -= damageReceived;
        if (currentHealth <= 0)
            Die();
        return true;
    }

    public virtual void TakeDamage(float phyDamage, float magDamage, Effect effect)
    {
        TakeDamage(phyDamage, magDamage);
        EffectController.AddEffect(effect,this);   
    }

    protected virtual void StagerLogic(float damage)
    {
        _stagerDamage += damage;

        if (_stager_time_left > 0 && _stagerDamage >= maxHealth * _stagerPercent)
        {
            Stager();
            _stager_time_left = 0;
            _stagerDamage = 0;
        }
        else if(_stager_time_left <= 0)
        {
            _stagerDamage = 0;
            _stager_time_left = 0;
        }

    }

    protected virtual void Stager()
    {

    }

    public virtual void Die()
    {
     
    }

}
