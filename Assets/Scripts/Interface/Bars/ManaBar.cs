using UnityEngine;
    

public class ManaBar : Bar
{
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.GetCurrentMana());
        SetMaxValue(PlayerStat.Instance.GetMaxMana());
    }

}