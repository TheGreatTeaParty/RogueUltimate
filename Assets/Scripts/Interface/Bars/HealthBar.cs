﻿using System.Collections;
using UnityEngine;
using TMPro;

public class HealthBar : Bar
{
    public TextMeshProUGUI Value;

    private void Start()
    { 
        CharacterManager.Instance.Stats.OnHealthChanged += ChangeSliderValue;
        ChangeSliderValue(CharacterManager.Instance.Stats.CurrentHealth);
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
        {
            if((int) _current == 0 && _current > 0)
                Value.SetText(1 + "/" + ((int)CharacterManager.Instance.Stats.Strength.MaxHealth.Value).ToString());
            else
                Value.SetText((int)_current + "/" + ((int)CharacterManager.Instance.Stats.Strength.MaxHealth.Value).ToString());
        }
    }
}