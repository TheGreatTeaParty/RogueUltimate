using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Lame")]
public class Lame : Trait
{
    public override void ApplyTrait()
    {
        if(CharacterManager.Instance.Stats)
            CharacterManager.Instance.Stats.playerMovement.DecreaseMovementSpeed(0.35f);
    }
    public override void DeleteTrait()
    {
        if (CharacterManager.Instance.Stats)
            CharacterManager.Instance.Stats.playerMovement.IncreaseMovementSpeed(0.35f);
    }
}
