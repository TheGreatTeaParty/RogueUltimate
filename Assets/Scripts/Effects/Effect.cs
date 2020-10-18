using UnityEngine;


public enum EffectType
{
    None = 0,
    Burning = 1,
    Freezing = 2,
}


public class Effect
{
    private float _intensity;
    private float _time;
    private EffectType _effectType;

    public EffectType EffectType => _effectType;
    public float Time
    {
        get => _time;
        set => _time = value;
    }
    
    public Effect(EffectType effectType, float intensity, float time)
    {
        _effectType = effectType;
        _intensity = intensity;
        _time = time;
    }

}