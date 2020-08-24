using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public string Name;
    public int level;
    [Space]
    public Stat physicalDamage;
    public Stat magicDamage;
    public Stat physicalProtection;
    public Stat magicProtection;
    [Space]

    [SerializeField] public int maxHealth = 100;
    public int currentHealth;
    
    protected int physicalDamageReceived;
    protected int magicDamageReceived;
    

    // This is base class for all NPS, Player, Enemy so on
    // all damage counting, damage intake should be written here
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public virtual void TakeDamage(int _physicalDamage,int _magicDamage)
    {
        physicalDamageReceived = (int)(_physicalDamage * (100 / (100 + (float)physicalProtection.GetValue())));
        magicDamageReceived = (int)(_magicDamage * (100 / (100 + (float)magicProtection.GetValue())));
        currentHealth -= (physicalDamageReceived + magicDamageReceived);
        if (currentHealth <= 0)
            Die();
    }
    
    public virtual void Die()
    {
     
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public string GetName()
    {
        return Name;
    }

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
    }
    

}
