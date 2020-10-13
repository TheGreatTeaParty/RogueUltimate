using System;
using UnityEngine;
using UnityEngine.UI;


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

    private AbilitySlot _draggedSlot;
    
    [SerializeField] private Image draggableAbility;
    [SerializeField] private AbilitySlot[] abilitySlots;
    [SerializeField] private AbilityQuickSlot[] abilityQuickSlots;
    

    private void Start()
    {
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
    }

    private void Click(AbilitySlot abilitySlot)
    {
        // show info
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

}