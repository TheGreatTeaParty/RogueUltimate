using UnityEngine;


public enum StatModifierType
{
    Flat = 10,
    PercentAdd = 20,
    PercentMult = 30
}


public class StatModifier : MonoBehaviour
{
    public readonly float Value;
    public readonly int Order;
    public readonly object Source;
    public readonly StatModifierType Type;

    public StatModifier(float value, StatModifierType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
    }
    
    public StatModifier(float value, StatModifierType type) : this (value, type, (int)type, null) { }
    
    public StatModifier(float value, StatModifierType type, int order) : this (value, type, order, null) { }
    
    public StatModifier(float value, StatModifierType type, object source) : this (value, type, (int)type, source) { }

}