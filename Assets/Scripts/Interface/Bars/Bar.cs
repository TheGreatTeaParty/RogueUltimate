using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    public void SetCurrentValue(float value)
    {
        slider.value = value;
    }
    
    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }

}