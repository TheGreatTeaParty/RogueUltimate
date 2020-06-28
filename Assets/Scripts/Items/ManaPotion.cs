using UnityEngine;

public class ManaPotion : UsableItem
{
    [SerializeField] private int manaBonus;
    
    
    public override void ModifyStats()
    {
        base.ModifyStats();
        PlayerStat.Instance.ModifyMana(manaBonus);
    }
    
} 