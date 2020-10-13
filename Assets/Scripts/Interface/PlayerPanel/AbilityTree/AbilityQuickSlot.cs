using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class AbilityQuickSlot : AbilitySlot
{
    public AbilitySlotOnScene abilitySlotOnScene;


    private void Start()
    {
        abilitySlotOnScene.OnClickEvent += Activate;
    }

    public void SetAbility(AbilityQuickSlot quickSlot)
    {
        if (quickSlot.Ability != null)
        {
            _image.sprite = quickSlot.Ability.Sprite;
            _image.color = Color.white;
            return;
        }

        _image.sprite = null;
        _image.color = Color.clear;
    }

    public void Activate(AbilitySlotOnScene abilitySlotOnScene)
    {
        var ability = Ability as ActiveAbility;
        if (ability == null)
            return;
        
        ability.Activate();
        abilitySlotOnScene.WakeCooldown(ability.coolDownTime);
    }
    
}