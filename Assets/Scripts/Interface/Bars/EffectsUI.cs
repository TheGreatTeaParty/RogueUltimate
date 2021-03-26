using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsUI : MonoBehaviour
{
    public Transform Template;
    private List<EffectSlot> _effectsUI;

    private void Start()
    {
        _effectsUI = new List<EffectSlot>();
        CharacterManager.Instance.Stats.EffectController.OnEffectChanged += HandleEffect;
    }

    private void HandleEffect(Effect effect, bool state)
    {
        if(state) //Create Icon;
        {
            var new_effect = Instantiate(Template, gameObject.transform);
            EffectSlot effect_slot = new_effect.GetComponent<EffectSlot>();
            effect_slot.SetEffect(effect);
            _effectsUI.Add(effect_slot);
        }
        else
        {
            var result = _effectsUI.Find(x => x.Effect.EffectName == effect.EffectName);
            if (result)
            {
                
                result.DeleteEffect();
                _effectsUI.Remove(result);
            }
        }
    }
}
