using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    protected const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 0.8f;

    protected float damagedHealthShrinkTimer;

    [SerializeField] protected Slider slider;
    [SerializeField] protected Slider changedslider;

    public void SetCurrentValue(float value)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        slider.value = value;
    }
    
    public virtual void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }


}