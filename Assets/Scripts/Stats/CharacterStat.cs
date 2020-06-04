using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public Stat damage;
    public Stat armor;
    [Space]
    public int MaxHealth = 100;

    protected int currenthealth { get; set; }
    protected int damage_recieved { get; set; }
    
    // This is base class for all NPS, Player, Enemy so on
    // all damage counting, damage intake should be written here
    private void Awake()
    {
        currenthealth = MaxHealth;
    }

    
    public virtual void TakeDamage(int damage)
    {
        damage_recieved = (int)(damage * (100 / (100 + (float)armor.GetValue())));
        currenthealth -= damage_recieved;
        if (currenthealth <= 0)
            Die();
    }
    


    
    public virtual void Die()
    {
        Debug.Log("Died");
    }
    

}
