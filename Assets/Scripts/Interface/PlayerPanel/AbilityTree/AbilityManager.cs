using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour
{
    #region Singleton
    
    public static AbilityManager Instance;
    
    void Awake()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    }

    #endregion
    public Action<SkillTree.TreeType> OnTreeChanged;
    public Action OnSkillTreeUpdated;

    private AbilitySlot _draggedSlot;
    
    [SerializeField] private Image draggableAbility;

    private AbilitySlot[] abilitySlots;
    private List<Ability> _unlockedAbilities;
    private SkillTree[] skillTrees;
    private PlayerStat _playerStat;

    [SerializeField] private AbilityQuickSlot[] abilityQuickSlots;
    [SerializeField] private AbilityTooltip _abilityTooltip;
    

    private void Start()
    {
        skillTrees = GetComponentsInChildren<SkillTree>();
        abilitySlots = GetComponentsInChildren<AbilitySlot>();
        _abilityTooltip.OnAbbilityUnlocked += UnlockAbility;
        OnTreeChanged += ChangeTheTree;
        if(_unlockedAbilities == null)
            _unlockedAbilities = new List<Ability>();
        _playerStat = CharacterManager.Instance.Stats;

        for (int i = 0; i < abilitySlots.Length; i++)
        {
            abilitySlots[i].OnClickEvent += Click;
            abilitySlots[i].OnBeginDragEvent += BeginDrag;
            abilitySlots[i].OnDragEvent += Drag;
            abilitySlots[i].OnEndDragEvent += EndDrag;
            abilitySlots[i].OnDropEvent += Drop;
        }

        for (int i = 0; i < abilityQuickSlots.Length; i++)
        {
            abilityQuickSlots[i].OnClickEvent += Click;
            abilityQuickSlots[i].OnBeginDragEvent += BeginDrag;
            abilityQuickSlots[i].OnDragEvent += Drag;
            abilityQuickSlots[i].OnEndDragEvent += EndDrag;
            abilityQuickSlots[i].OnDropEvent += Drop;
        }
        Invoke("InvokeOnStart", 0.3f);
    }

    private void Click(AbilitySlot abilitySlot)
    {
        // show info
        _abilityTooltip.gameObject.SetActive(true);
        if(_playerStat.SkillPoints > 0)
            _abilityTooltip.OpenTooltip(abilitySlot, abilitySlot.IsLocked,abilitySlot._isUpgraded);
        else
            _abilityTooltip.OpenTooltip(abilitySlot, true, false);
    }
    private void InvokeOnStart()
    {
        UnlcokSavedAbility();
        OnTreeChanged?.Invoke(SkillTree.TreeType.Fire);
    }
    private void BeginDrag(AbilitySlot abilitySlot)
    {
        if (abilitySlot.Ability == null) return;

        _draggedSlot = abilitySlot;
        draggableAbility.sprite = _draggedSlot.Ability.Sprite;
        draggableAbility.transform.position = Input.mousePosition;
        draggableAbility.enabled = true;
    }

    private void Drag(AbilitySlot abilitySlot)
    {
        if (draggableAbility.enabled)
            draggableAbility.transform.position = Input.mousePosition;
    }

    private void EndDrag(AbilitySlot abilitySlot)
    {
        _draggedSlot = null;
        draggableAbility.enabled = false;
    }

    private void Drop(AbilitySlot dropAbilitySlot)
    {
        if (dropAbilitySlot == null || _draggedSlot == null)
            return;
        
        var dropSlot = dropAbilitySlot as AbilityQuickSlot;
        if (dropSlot == null)
            return;

        var dragSlot = _draggedSlot as AbilityQuickSlot;
        if (dragSlot == null)
        {
            for (int i = 0; i < abilityQuickSlots.Length; i++)
                if (abilityQuickSlots[i].Ability == _draggedSlot.Ability)
                    return;
            
            dropSlot.Ability = _draggedSlot.Ability;
            dropSlot.abilitySlotOnScene.SetAbility(dropSlot);
            return;
        }

        Ability draggedAbility = dragSlot.Ability;
        
        dragSlot.Ability = dropSlot.Ability;
        dragSlot.abilitySlotOnScene.SetAbility(dragSlot);
        
        dropSlot.Ability = draggedAbility;
        dropSlot.abilitySlotOnScene.SetAbility(dropSlot);
    }

    public void ChangeTheTree(SkillTree.TreeType type)
    {
        OnSkillTreeUpdated?.Invoke();

        foreach (SkillTree tree in skillTrees)
        {
            if (tree.Type == type)
            {
                tree.gameObject.SetActive(true);
            }
            else
            {
                tree.gameObject.SetActive(false);
            }
        }
    }

    private void UnlockAbility(AbilitySlot abilitySlot)
    {
        abilitySlot.UpgradeSkill();
        _playerStat.SkillPoints -= 1;
        _unlockedAbilities.Add(abilitySlot.Ability);
        OnSkillTreeUpdated?.Invoke();
    }
    private void UnlcokSavedAbility()
    {
        foreach (Ability ability in _unlockedAbilities)
        {
            foreach (SkillTree tree in skillTrees)
            {
                tree.UnlockUpgradedAbility(ability);
            }
        }
    }
    public void CreateAbilityList()
    {
        _unlockedAbilities = new List<Ability>();
    }
    public void AddSavedAbility(Ability ability)
    {
        _unlockedAbilities.Add(ability);
    }
    public List<Ability> GetUnlockedAbilities()
    {
        return _unlockedAbilities;
    }
    public AbilityQuickSlot[] GetQuickSlots()
    {
        return abilityQuickSlots;
    }
    public void AddQuickSlotAbility(Ability ability, int index)
    {
        abilityQuickSlots[index].Ability = ability;
        abilityQuickSlots[index].abilitySlotOnScene.SetAbility(abilityQuickSlots[index]);
    }
}