using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/PureAbility")] 
public class Ability : ScriptableObject
{
    protected LayerMask _enemyMask;
    public int ID;
    public SkillTree.TreeType Type;

    [SerializeField] protected string abilityName;
    [TextArea]
    [SerializeField] protected string description;
    [SerializeField] protected Sprite sprite;

    public string AbilityName
    {
        get => LocalizationSystem.GetLocalisedValue(abilityName.Replace(' ', '_'));
    }
    public string Description
    {
        get => LocalizationSystem.GetLocalisedValue(abilityName.Replace(' ', '_')+"_desc");
    }
    public Sprite Sprite => sprite;

}