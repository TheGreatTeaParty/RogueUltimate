using UnityEngine;
using System;


public enum EffectType
{
    Poison = 0,
    Daze,
    Stun,
    Bleed,
    Fire,
    Freeze,
    Curse,
    Natural,
    Fury,
}

[System.Serializable]
public class Effect:ScriptableObject
{
    public String EffectName;
    public Sprite Icon;
    [SerializeField]
    protected float _intensity;
    [SerializeField]
    protected int _ticks;

    public EffectType _effectType;
    [Range(0f, 1f)]
    public float _chance;
    [HideInInspector]
    public CharacterStat _stat;

    //Visual Effect:
    [SerializeField] public Transform _effectFX;
    public Action OnDelete;

    public EffectType EffectType => _effectType;
    public int Ticks
    {
        get => _ticks;
        set => _ticks = value;
    }
    
    public virtual void ApplyEffect()
    {
        if (_ticks > 0)
            _ticks--;
    }

    public virtual void RemoveEffect()
    {
        OnDelete?.Invoke();
        OnDelete = null;
        _stat = null;
        Destroy(this);
    }

    public void CreateFX()
    {
        SpriteRenderer sprite = _stat.gameObject.GetComponent<SpriteRenderer>();
        Transform fx = Instantiate(_effectFX, sprite.transform.position, Quaternion.identity);
        EffectFX effectFX = fx.GetComponent<EffectFX>();
        effectFX.target = sprite.gameObject;
        effectFX.targetSprite = sprite;
        effectFX.effect = this;

    }

}