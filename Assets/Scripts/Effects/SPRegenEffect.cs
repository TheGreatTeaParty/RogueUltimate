using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/SPRegenEffect")]
public class SPRegenEffect : Effect
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyStamina(_intensity);
    }
}
