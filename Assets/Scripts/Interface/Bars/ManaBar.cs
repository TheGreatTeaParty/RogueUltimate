public class ManaBar : Bar
{
    private void Update()
    {
        SetCurrentValue(CharacterManager.Instance.Stats.CurrentMana);
        SetMaxValue(CharacterManager.Instance.Stats.MaxMana);
    }

}