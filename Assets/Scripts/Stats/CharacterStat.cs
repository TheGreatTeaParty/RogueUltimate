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
    protected float currentHealth;
    protected float damageReceived;

    // Effects 
    public EffectController EffectController;
    protected float _timeLeft;
    protected const float TICK_TIME = 1f;

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
    }

    protected virtual void Update()
    {
        if(_timeLeft <= 0)
        {
            EffectController.Tick();
            _timeLeft = TICK_TIME;
        }
        _timeLeft -= Time.deltaTime;
    }

    protected virtual void TakeEffectDamage(Effect effect)
    {

    }

    protected virtual void ChangeEffect(Effect oldEffect, Effect newEffect)
    {
        
    }
    
    public virtual bool TakeDamage(float _phyDamage, float _magDamage)
    {
        float phyDamage = (_phyDamage * (100 / (100 + physicalProtection.Value)));
        float magDamage = (_magDamage * (100 / (100 + magicProtection.Value)));
        damageReceived = (phyDamage + magDamage);
        currentHealth -= damageReceived;
        if (currentHealth <= 0)
            Die();
        return true;
    }

    public virtual void TakeDamage(float phyDamage, float magDamage, Effect effect)
    {
        TakeDamage(phyDamage, magDamage);
        EffectController.AddEffect(effect);   
    }

    public virtual void Die()
    {
     
    }

}
