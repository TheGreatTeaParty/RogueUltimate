using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/Lame")]
public class Lame : Trait
{
    public override void ApplyTrait()
    {
        if(CharacterManager.Instance.Stats)
            CharacterManager.Instance.Stats.playerMovement.ModifyBaseSpeed(-1f);
    }
    public override void DeleteTrait()
    {
        if (CharacterManager.Instance.Stats)
            CharacterManager.Instance.Stats.playerMovement.ModifyBaseSpeed(1f);
    }
}
