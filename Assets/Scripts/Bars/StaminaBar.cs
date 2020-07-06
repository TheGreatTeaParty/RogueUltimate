using UnityEngine;


public class StaminaBar : Bar
{
    private void Start()
    {
        SetMaxValue(PlayerStat.Instance.maxStamina);
    }
    
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.currentStamina);
    }
    
}