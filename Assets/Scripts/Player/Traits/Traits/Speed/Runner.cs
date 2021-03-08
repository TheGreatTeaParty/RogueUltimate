using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Runner")]
public class Runner : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.playerMovement.BASE_MOVEMENT_SPEED *= 2;
    }
}
