using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public Stat physicalDamage;
    public Stat magicDamage;
    public Stat physicalProtection;
    public Stat magicProtection;
    [Space]
    public int maxHealth = 100;

    protected int currentHealth { get; set; }
    protected int physicalDamageReceived { get; set; }
    protected int magicDamageReceived { get; set; }
    
    
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
        Debug.Log("Died");
    }
    

}
