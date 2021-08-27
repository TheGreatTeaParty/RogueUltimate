using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalCtone : MonoBehaviour,IInteractable
{
    public string GetActionName()
    {
        return "Use";
    }

    public void Interact()
    {
        var player = CharacterManager.Instance.Stats;
        AudioManager.Instance.Play("MagicalStone");
        player.Intelligence.MaxMana.AddModifier(new StatModifier(.5f, StatModifierType.PercentAdd));
        Destroy(this);
    }
}
