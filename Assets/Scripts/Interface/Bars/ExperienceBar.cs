using UnityEngine;
using TMPro;

public class ExperienceBar : Bar
{
    [SerializeField]
    TextMeshProUGUI Level;

    [SerializeField]
    TextMeshProUGUI LevelBorder;

    [SerializeField]
    GameObject VisualObject;

    float _displayTime = 3.5f;
    float _timeLeft;

    private void Start()
    {
        CharacterManager.Instance.Stats.OnXPChanged += ChangeSliderValue;
        DisableXPBar();
    }

    void Update()
    {
        int level = CharacterManager.Instance.Stats.Level;
        Level.text = (level).ToString();
        LevelBorder.text = (level).ToString();

        damagedHealthShrinkTimer -= Time.deltaTime;
        _timeLeft -= Time.deltaTime;

        if (damagedHealthShrinkTimer < 0)
        {
            if (slider.value < changedslider.value)
            {
                float shrinkSpeed = 100f;
                slider.value += shrinkSpeed * Time.deltaTime;
            }
        }

        if(_timeLeft < 0)
        {
            DisableXPBar();
        }

        switch (level)
        {
            // * 2
            case 1: { SetMaxValue(220); break; }
            case 2: { SetMaxValue(440); break; }
            case 3: { SetMaxValue(700); break; }
            case 4: { SetMaxValue(980); break; }
            case 5: { SetMaxValue(1300); break; }
            case 6: { SetMaxValue(1640); break; }
            case 7: { SetMaxValue(2020); break; }
            case 8: { SetMaxValue(2460); break; }
            case 9: { SetMaxValue(2920); break; }
            case 10: { SetMaxValue(3440); break; }
            case 11: { SetMaxValue(4000); break; }
            case 12: { SetMaxValue(4640); break; }
            case 13: { SetMaxValue(5340); break; }
        }
        
    }

    private void ChangeSliderValue(float value)
    {
        TurnXPBar();
        changedslider.value = value;
        if (value < slider.value)
            slider.value = value;
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
    }

    private void DisableXPBar()
    {
        if (VisualObject.activeSelf)
        {
            VisualObject.SetActive(false);
            changedslider.gameObject.SetActive(false);
        }
        return;
    }

    private void TurnXPBar()
    {
        _timeLeft = _displayTime;
        VisualObject.SetActive(true);
        changedslider.gameObject.SetActive(true);
    }

    public override void SetMaxValue(float value)
    {
        slider.maxValue = value;
        changedslider.maxValue = value;
    }
}
