using UnityEngine;
    

public class ManaBar : Bar
{
    private void Start()
    {
        SetMaxValue(PlayerStat.Instance.maxMana);
    }
    
    private void Update()
    {
        SetCurrentValue(PlayerStat.Instance.currentMana);
    }

}