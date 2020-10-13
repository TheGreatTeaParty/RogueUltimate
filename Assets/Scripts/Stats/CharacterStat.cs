using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStat : MonoBehaviour
{
    [FormerlySerializedAs("Name")] 
    public string characterName;
    protected int level;
    [Space] 
    public Stat physicalDamage;
    public Stat physicalProtection;
    public Stat magicDamage;
    public Stat magicProtection;
    [Space]
    [SerializeField] protected float maxHealth = 100;
    protected float currentHealth;
    protected float damageReceived;

    public string CharacterName
    {
        get => characterName;
        set => characterName = value;
    }
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
    public float DamageReceived
    {
        get => damageReceived;
        set => damageReceived = value;
    }

    
    // This is base class for all NPS, Player, Enemy so on
    // all receivedPhysicalDamage counting, receivedPhysicalDamage intake should be written here
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public virtual void TakeDamage(float receivedPhysicalDamage, float receivedMagicDamage)
    {
        receivedPhysicalDamage = (physicalDamage.Value * (100 / (100 + physicalProtection.Value)));
        receivedMagicDamage = (magicDamage.Value * (100 / (100 + magicProtection.Value)));
        currentHealth -= (receivedPhysicalDamage + receivedMagicDamage);
        if (currentHealth <= 0)
            Die();
    }

    public virtual void TakeDamage(float receivedPhyDmg, float receivedMagDmg, Vector2 bounceDirection, float power)
    {

    }
    
    public virtual void Die()
    {
     
    }

}
