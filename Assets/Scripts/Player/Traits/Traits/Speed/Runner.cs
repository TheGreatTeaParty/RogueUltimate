using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Runner")]
public class Runner : Trait
{
    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.playerMovement.IncreaseMovementSpeed(1.25f);
    }
    public override void DeleteTrait()
    {
        CharacterManager.Instance.Stats.playerMovement.DecreaseMovementSpeed(1.25f);
    }
}
