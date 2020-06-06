using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public Stat ph_damage;
    public Stat mg_damage;
    public Stat ph_armor;
    public Stat mg_armor;
    [Space]
    public int MaxHealth = 100;

    protected int currenthealth { get; set; }
    protected int ph_damage_recieved { get; set; }
    protected int mg_damage_recieved { get; set; }

    // This is base class for all NPS, Player, Enemy so on
    // all damage counting, damage intake should be written here
    private void Awake()
    {
        currenthealth = MaxHealth;
    }

    
    public virtual void TakeDamage(int ph_damage,int mg_damage)
    {
        ph_damage_recieved = (int)(ph_damage * (100 / (100 + (float)ph_armor.GetValue())));
        mg_damage_recieved = (int)(mg_damage * (100 / (100 + (float)mg_armor.GetValue())));
        currenthealth -= (ph_damage_recieved + mg_damage_recieved);
        if (currenthealth <= 0)
            Die();
    }
    


    
    public virtual void Die()
    {
        Debug.Log("Died");
    }
    

}
