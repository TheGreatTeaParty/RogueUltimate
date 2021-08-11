using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/PureAbility")] 
public class Ability : ScriptableObject
{
    protected LayerMask _enemyMask;
    public int ID;
    public SkillTree.TreeType Type;

    [SerializeField] protected String abilityName;
    [SerializeField] protected String description;
    [SerializeField] protected Sprite sprite;

    public String AbilityName => abilityName;
    public string Description => description;
    public Sprite Sprite => sprite;

}