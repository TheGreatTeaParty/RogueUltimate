using UnityEngine;
public class StaminaBar : Bar
{
    private void Start()
    {
        CharacterManager.Instance.Stats.OnStaminaChanged += SetStaminaValue;
    }

    private void Update()
    {
        SetMaxValue(CharacterManager.Instance.Stats.MaxStamina);
    }

    private void SetStaminaValue(float value)
    {
        SetCurrentValue(value);
    }
}