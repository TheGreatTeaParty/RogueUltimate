using System;
using System.Collections.Generic;
using UnityEngine;


public class EffectController : MonoBehaviour
{
    private List<Effect> _effects;
    public List<Effect> Effects => _effects;

    
    public event Action<Effect, Effect> OnEffectChanged;


    private void Awake()
    {
        _effects = new List<Effect>();
    }

    public void Add(EffectType effectType, float intensity, float time)
    {
        for (int i = 0; i < _effects.Count; i++)
            if (effectType == _effects[i].EffectType)
            {
                _effects[i].Time = time;
                return;
            }

        var effect = new Effect(effectType, intensity, time);
        _effects.Add(effect);
        OnEffectChanged?.Invoke(null, effect);
    }

    private void Update()
    {
        for (int i = 0; i < _effects.Count; i++)
        {
            _effects[i].Time -= Time.deltaTime;
            if (_effects[i].Time < 0)
            {
                OnEffectChanged?.Invoke(_effects[i], null);
                _effects.RemoveAt(i);
            }
        }
    }
    
}