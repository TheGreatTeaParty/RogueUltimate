public class StaminaBar : Bar
{
    private void Update()
    {
        SetCurrentValue(CharacterManager.Instance.Stats.CurrentStamina);
        SetMaxValue(CharacterManager.Instance.Stats.MaxStamina);
    }
    
}