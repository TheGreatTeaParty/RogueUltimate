using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Dystrophic")]
public class Dystrophic : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Strength.MakeAbsolute();
    }
}
