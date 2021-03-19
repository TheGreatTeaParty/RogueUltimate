using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


[System.Serializable]
public class Stat
{
    protected readonly List<StatModifier> _statModifiers;
    public readonly ReadOnlyCollection<StatModifier> statModifiers;

    protected bool _isDirty = true;
    protected float _value;
    protected float _lastBaseValue = float.MinValue;

    [SerializeField] private float baseValue;
    
    public virtual float Value
    {
        get
        {
            if (_isDirty || baseValue != _lastBaseValue)
            {
                _lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                _isDirty = false;
            }

            return _value;
        }
    }


    public Stat()
    {
        _statModifiers = new List<StatModifier>();
        statModifiers = _statModifiers.AsReadOnly();
    }

    public Stat(float baseValue) : this ()
    {
        this.baseValue = baseValue;
    }

    public void SETBASE(float value)
    {
        baseValue = value;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;
        
        for (int i = 0; i < _statModifiers.Count; i++)
        {
            StatModifier modifier = _statModifiers[i];
            switch (modifier.Type)
            {
                case StatModifierType.Flat:
                    {
                        finalValue += modifier.Value;
                        break;
                    }
                
                case StatModifierType.PercentAdd:
                {
                    sumPercentAdd += modifier.Value;
                    if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }

                    break;
                }
                
                default:
                    finalValue *= 1 + modifier.Value;
                    break;
            }

           // finalValue += _statModifiers[i].Value; Do we need it?
        }
        
        return (float) Math.Round(finalValue, 1);
    }
    
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        
        if (a.Order > b.Order)
            return 1;

        return 0;
    }
    
    public virtual void AddModifier(StatModifier statModifier)
    {
        _isDirty = true;
        _statModifiers.Add(statModifier);
        _statModifiers.Sort(CompareModifierOrder);
    }
    
    public virtual bool RemoveModifier(StatModifier statModifier)
    {
        if (_statModifiers.Remove(statModifier))
        {
            _isDirty = true;
            return _statModifiers.Remove(statModifier); 
        }

        return false;
    }
    public virtual bool RemoveLast()
    {
        if (_statModifiers.Count > 0)
        {
            _statModifiers.RemoveAt(_statModifiers.Count-1);
            _isDirty = true;
            return true;
        }

        return false;
    }


    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool doneRemove = false;
        
        for (int i = _statModifiers.Count - 1; i >= 0; i--)
        {
            if (_statModifiers[i].Source == source)
            {
                _isDirty = true;
                doneRemove = true;
                _statModifiers.RemoveAt(i);
            }
        }

        return doneRemove;
    }

}
