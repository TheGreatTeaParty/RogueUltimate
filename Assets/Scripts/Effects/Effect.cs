using UnityEngine;



public enum EffectType
{
    Natural = 0,
    Physical = 1,
    Elemental = 2,
}

[System.Serializable]
public class Effect
{
    protected float _intensity;
    protected int _ticks;
    protected EffectType _effectType;

    public EffectType EffectType => _effectType;
    public int Ticks
    {
        get => _ticks;
        set => _ticks = value;
    }
    
    public Effect(float intensity, int ticks = 0)
    {
        _intensity = intensity;
        _ticks = ticks;
    }

    public virtual void ApplyEffect()
    {
        if (_ticks > 0)
            _ticks--;
    }
    public virtual void RemoveEffect()
    {

    }

}