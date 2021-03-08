using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Idiot")]
public class Idiot : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.Intelligence.MakeAbsolute();
    }
}
