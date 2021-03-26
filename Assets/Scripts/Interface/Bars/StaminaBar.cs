using System.Collections;
using UnityEngine;
using TMPro;
public class StaminaBar : Bar
{
    public TextMeshProUGUI Value;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.0001f);
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
        SetTextValue(value);
    }
    private void SetTextValue(float _current)
    {
        if(Value)
            Value.SetText(_current + "/" + CharacterManager.Instance.Stats.Agility.MaxStamina.Value.ToString());
    }
}