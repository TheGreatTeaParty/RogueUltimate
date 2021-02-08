using System;
using System.Collections.Generic;
using UnityEngine;


public class EffectController
{
    private List<Effect> _effects;
    public List<Effect> Effects => _effects;

    public EffectController()
    {
        _effects = new List<Effect>();
    }

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }

    public void RemoveEffectsOfType(EffectType type)
    {
        for(int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].EffectType == type)
            {
                _effects[i].RemoveEffect();
                _effects.RemoveAt(i);
            }
        }
    }
    public void Tick()
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].Ticks > 0)
                _effects[i].ApplyEffect();
            else
            {
                _effects[i].RemoveEffect();
                _effects.RemoveAt(i);
            }
        }
    }
    
}