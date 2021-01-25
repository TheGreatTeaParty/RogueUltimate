using UnityEngine;
public class StaminaBar : Bar
{
    private void Start()
    {
        CharacterManager.Instance.Stats.OnStaminaChanged += SetStaminaValue;
    }

    private void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;

        if (damagedHealthShrinkTimer < 0)
        {
            if (slider.value < changedslider.value)
            {
                float shrinkSpeed = 100f;
                changedslider.value -= shrinkSpeed * Time.deltaTime;
            }
        }

        SetMaxValue(CharacterManager.Instance.Stats.Agility.MaxStamina.Value);
    }

    private void SetStaminaValue(float value)
    {
        if (slider.value < value)
            changedslider.value = value;
        SetCurrentValue(value);
    }
}