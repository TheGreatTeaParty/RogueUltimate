using UnityEngine;
    

public class ManaBar : Bar
{
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.CurrentMana);
        SetMaxValue(PlayerStat.Instance.MaxMana);
    }

}