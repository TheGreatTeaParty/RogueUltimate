using System;
using System.Collections.Generic;
using UnityEngine;


public class EffectController
{
    private List<Effect> _effects;
    public List<Effect> Effects => _effects;
    public Action<Effect,bool> OnEffectChanged;

    public EffectController()
    {
        _effects = new List<Effect>();

    }

    public void AddEffect(Effect effect,CharacterStat character)
    {
        effect._stat = character;
        if (!character.IsEffectApplied(effect._chance, effect._effectType)) { return; }
      
        Predicate<Effect> temp = cur_effect => cur_effect.EffectType == effect.EffectType;
        if (!_effects.Find(temp))
        {
            _effects.Add(effect);
            effect.CreateFX();
            OnEffectChanged?.Invoke(effect,true);
        }

    }

    public void RemoveEffectsOfType(EffectType type)
    {
        for(int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].EffectType == type)
            {
                Effect effect = _effects[i];
                OnEffectChanged?.Invoke(effect,false);
                _effects.Remove(_effects[i]);
                effect.RemoveEffect();
            }
        }
    }
    public void Tick()
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].Ticks > 0)
                _effects[i].ApplyEffect();

            else if(_effects[i].Ticks == 0)
            {
                Effect effect = _effects[i];
                OnEffectChanged?.Invoke(effect, false);
                _effects.Remove(_effects[i]);
                effect.RemoveEffect();
            }
        }
    }

    public void RemoveAll()
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            _effects[i].RemoveEffect();
        }
    }
    
}