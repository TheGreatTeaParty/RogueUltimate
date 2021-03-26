using System.Collections;
using UnityEngine;
using TMPro;

public class HealthBar : Bar
{
    public TextMeshProUGUI Value;

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
        SetMaxValue(CharacterManager.Instance.Stats.Strength.MaxHealth.Value);
    }

    private void ChangeSliderValue(float value)
    {
        if (slider.value < value)
            changedslider.value = value;
        SetCurrentValue(value);
        SetTextValue(value);
    }
    private void SetTextValue(float _current)
    {
        if (Value)
            Value.SetText((int)_current + "/" + ((int)CharacterManager.Instance.Stats.Strength.MaxHealth.Value).ToString());
    }
}