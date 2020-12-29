using UnityEngine;
public class ManaBar : Bar
{

    private void Start()
    {
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

        SetMaxValue(CharacterManager.Instance.Stats.MaxMana);
    }

    private void SetManaValue(float value)
    {
        if (slider.value < value)
            changedslider.value = value;
        SetCurrentValue(value);
    }
}