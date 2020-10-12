using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceBar : Bar
{
    [SerializeField]
    TextMeshProUGUI Level;

    [SerializeField]
    TextMeshProUGUI LevelBorder;

    void Update()
    {
        int level = PlayerStat.Instance.Level;
        Level.text = (level).ToString();
        LevelBorder.text = (level).ToString();

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
            case 13 : { SetMaxValue(5340); break; }
        }
        SetCurrentValue(PlayerStat.Instance.XP);
        
    }
}
