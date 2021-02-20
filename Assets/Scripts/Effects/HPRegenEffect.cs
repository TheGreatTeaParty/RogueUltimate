using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/HPRegenEffect")]
public class HPRegenEffect : Effect
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyHealth(_intensity);
    }
}
