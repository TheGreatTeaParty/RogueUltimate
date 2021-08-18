using UnityEngine;


public class AbilityQuickSlot : AbilitySlot
{
    public AbilitySlotOnScene abilitySlotOnScene;


    private void Start()
    {
        abilitySlotOnScene.OnClickEvent += Activate;
    }

    public void SetAbility(Ability ability)
    {
        if (ability != null)
        {
            _image.sprite = ability.Sprite;
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