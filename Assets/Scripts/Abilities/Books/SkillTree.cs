using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public enum TreeType
    {
        Fire,
        Berserk,
        Rober,
    }
    [SerializeField]
    private TreeType _type;
    public TreeType Type
    {
        get => _type;
        set => _type = value;
    }
    [SerializeField]
    private AbilitySlot _firstAbility;
    private bool _isUpdated = false;
    private int _savedID;

    private void Start()
    {
        CharacterManager.Instance.Stats.OnSkillPointGained += UpdateUI;
        AbilityManager.Instance.OnSkillTreeUpdated += CheckUnlockedSkills;
    }
    private void UpdateUI()
    {
        _isUpdated = false;
    }
    private void CheckUnlockedSkills()
    {
        if (_isUpdated == false)
        {
            UnlockPotentialSkills(_firstAbility);
            _isUpdated = true;
        }
    }

    private void UnlockPotentialSkills(AbilitySlot slot)
    {
        if (!slot)
            return;
        if (slot.IsLocked) { slot.IsLocked = false; return; }
        else if(slot._isUpgraded)
        {
            for (int i = 0; i < slot.NextAbilities.Length; ++i)
            {
                UnlockPotentialSkills(slot.NextAbilities[i]);
            }
        }
    }
    public void UnlockUpgradedAbility(Ability ability)
    {
        if (ability.Type != Type)
            return;
        else
        {
            _savedID = ability.ID;
            UnlockDFS(_firstAbility);
        }
    }

    private bool UnlockDFS(AbilitySlot slot)
    {
        if (slot.Ability.ID == _savedID)
        {
            UnlockVisualy(slot);
            return true;
        }
        for (int i = 0; i < slot.NextAbilities.Length; ++i)
        {
            bool result = UnlockDFS(slot.NextAbilities[i]);
            if(result)
            {
                UnlockVisualy(slot);
                return true;
            }
        }
        return false;
    }

    private void UnlockVisualy(AbilitySlot slot)
    {
        slot.IsLocked = false;
        slot.UpgradeSkill();
    }
    

}
