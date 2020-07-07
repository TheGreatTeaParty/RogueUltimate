using System;
using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    public void SetCurrentValue(int value)
    {
        slider.value = value;
    }


    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }


}