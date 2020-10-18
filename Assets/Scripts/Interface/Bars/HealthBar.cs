public class HealthBar : Bar
{
    private void Update()
    {
        SetCurrentValue(CharacterManager.Instance.Stats.CurrentHealth);
        SetMaxValue(CharacterManager.Instance.Stats.MaxHealth);
    }
    
}