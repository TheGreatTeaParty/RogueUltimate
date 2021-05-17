using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Lame")]
public class Lame : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.playerMovement.BASE_MOVEMENT_SPEED /= 1.3f;
    }
}
