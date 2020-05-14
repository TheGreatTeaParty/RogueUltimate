
public enum StatModifierType
{
    Flat,
    Percent,
}


public class StatModifier
{
    public readonly float Value;
    public readonly StatModifierType Type;
    public readonly int Order;

    
    public StatModifier(float value, StatModifierType type, int order)
    {
        Value = value;
        Type = type;
        Order = order;
    }

    public StatModifier(float value, StatModifierType type) : this(value, type, (int) type)
    {
        
    }
    
}
