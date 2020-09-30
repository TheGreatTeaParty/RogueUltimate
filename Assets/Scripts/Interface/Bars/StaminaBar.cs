using UnityEngine;


public class StaminaBar : Bar
{
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.CurrentStamina);
        SetMaxValue(PlayerStat.Instance.MaxStamina);
    }
    
}