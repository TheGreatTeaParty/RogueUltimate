using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityTooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _abilityName;
    [SerializeField]
    private TextMeshProUGUI _abilityDescription;
    [SerializeField]
    private Image _abilityIcon;
    [SerializeField]
    private Button _unlock_btn;

    private AbilitySlot _selectedAbility;

    public delegate void AbillityUnlocked(AbilitySlot abilitySlot);
    public event AbillityUnlocked OnAbbilityUnlocked;

    public void OpenTooltip(AbilitySlot abilitySlot, bool Islocked, bool IsUpgraded)
    {
        _selectedAbility = abilitySlot;
        _abilityName.text = abilitySlot.Ability.AbilityName;
        _abilityDescription.text = abilitySlot.Ability.Description;
        _abilityIcon.sprite = abilitySlot.Ability.Sprite;

        if (!Islocked && !IsUpgraded)
            _unlock_btn.interactable = true;
        else
            _unlock_btn.interactable = false;
    }

    public void UnlockAbillity()
    {
        OnAbbilityUnlocked?.Invoke(_selectedAbility);
    }
}
