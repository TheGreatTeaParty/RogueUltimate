using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/DwarvenCurse")]
public class DwarvenCurse : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Agility.dodgeChance.SETBASE(0f);
        CharacterManager.Instance.Stats.PhysicalProtection.AddModifier(new StatModifier(4f, StatModifierType.Flat));
    }
}
