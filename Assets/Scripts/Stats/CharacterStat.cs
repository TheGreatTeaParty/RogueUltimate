using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public Stat damage;
    public Stat armor;

    [Space]
    public int MaxHealth = 100;

    private int currenthealth { get; set; }

    /* This is base class for all NPS, Player, Enemy so on
     * all damage counting, damage intake should be writen here
     */
    private void Awake()
    {
        currenthealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        if(currenthealth <= 0)
        {
            Die();
        }
    }

    public void ToDamage()
    {
        Debug.Log($"Damage Done: {damage.GetValue()}");
    }

    public virtual void Die()
    {
        Debug.Log("Died");
    }

}
