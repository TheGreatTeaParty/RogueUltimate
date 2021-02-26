using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/MPRegenEffect")]
public class MPRegEffect : Effect
{   
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        CharacterManager.Instance.Stats.ModifyMana(_intensity);
    }
}
