using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Traits/FlaimingHeart")]
public class Flamingheart : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Intelligence.FreezeResistance.AddModifier(new StatModifier(0.5f, StatModifierType.PercentAdd));
    }
}
