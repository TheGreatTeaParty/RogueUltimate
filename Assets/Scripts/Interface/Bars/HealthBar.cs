using UnityEngine;

public class HealthBar : Bar
{
    private void Start()
    {
        CharacterManager.Instance.Stats.OnHealthChanged += ChangeSliderValue;
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
        SetMaxValue(CharacterManager.Instance.Stats.MaxHealth);
    }

    private void ChangeSliderValue(float value)
    {
        SetCurrentValue(value);
    }
    
}