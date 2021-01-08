using System.Collections;
using UnityEngine;

public class HealthBar : Bar
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.0001f);
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
        if (slider.value < value)
            changedslider.value = value;
        SetCurrentValue(value);
    }
    
}