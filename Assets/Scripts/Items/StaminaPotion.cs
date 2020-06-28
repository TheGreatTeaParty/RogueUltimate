using UnityEngine;

public class StaminaPotion : UsableItem
{
    [SerializeField] private int staminaBonus;


    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyStamina(staminaBonus);
    }
    
}