using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Mad")]
public class Mad : Trait
{
    PlayerStat playerStat;
    bool _isApplied = false;

    public override void ApplyTrait()
    {
        playerStat = CharacterManager.Instance.Stats;
        playerStat.OnHealthChanged += Rage;
    }

    private void Rage(float current_healt)
    {
        if(current_healt < playerStat.Strength.MaxHealth.Value * 0.1f && !_isApplied)
        {
            playerStat.PhysicalDamage.AddModifier(new StatModifier(1.5f, StatModifierType.PercentAdd, this));
            _isApplied = true;
        }
        else if(current_healt >= playerStat.Strength.MaxHealth.Value * 0.1f)
        {
            playerStat.PhysicalDamage.RemoveAllModifiersFromSource(this);
            _isApplied = false;
        }

    }
}
