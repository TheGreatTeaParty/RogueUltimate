using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trait : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public TraitType Type;
    [TextArea]
    public string Description;

    public enum TraitType
    {
        zero = 0,
        price,
        speed,
        resistance,
        cooldown,
        disabillity,
        gamestart,
        damage,
        mechanics,
        protection,
        evade,
        OZOMOV,
    };

    public virtual void ApplyTrait(){}
    public virtual void DeleteTrait(){}
}
