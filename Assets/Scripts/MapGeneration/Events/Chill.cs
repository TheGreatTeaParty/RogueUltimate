using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chill : MonoBehaviour,IInteractable
{
    public string GetActionName()
    {
        return "Rest";
    }

    public void Interact()
    {
        var player = CharacterManager.Instance.Stats;
        player.Agility.MaxStamina.AddModifier(new StatModifier(.5f, StatModifierType.PercentAdd));
        AudioManager.Instance.Play("Rest");
        Destroy(this);
    }
}
