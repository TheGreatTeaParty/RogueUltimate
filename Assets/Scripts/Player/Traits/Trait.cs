using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trait : ScriptableObject
{
    public int ID;
    public Sprite Icon;
    public TraitType Type;
    [SerializeField]
    private string _name;
    [TextArea]
    [SerializeField]
    private string _description;
    [TextArea]
    [SerializeField]
    private string _briefDescription;

    public string Name
    {
        get => LocalizationSystem.GetLocalisedValue(_name.Replace(' ', '_'));
    }
    public string Description
    {
        get => LocalizationSystem.GetLocalisedValue(_name.Replace(' ', '_')+"_desc");
    }
    public string BriefDescription
    {
        get => LocalizationSystem.GetLocalisedValue(_name.Replace(' ', '_') + "_brief_desc");
    }

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
