using System.Collections;
using UnityEngine;
public class ManaBar : Bar
{

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.0001f);
        CharacterManager.Instance.Stats.OnManaChanged += SetManaValue;
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

        SetMaxValue(CharacterManager.Instance.Stats.Intelligence.MaxMana.Value);
    }

    private void SetManaValue(float value)
    {
        if (slider.value < value)
            changedslider.value = value;
        SetCurrentValue(value);
    }
}