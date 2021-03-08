using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Retarded")]
public class Retarded : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Agility.MakeAbsolute();
    }
}
