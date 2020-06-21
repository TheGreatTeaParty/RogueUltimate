using System;
using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    public void SetCurrentHealth(int value)
    {
        slider.value = value;
    }


    protected void SetMaxHealth(int value)
    {
        slider.maxValue = value;
    }


    public void LevelUp(int value)
    {
        slider.maxValue = PlayerStat.Instance.maxHealth;
    }
    

}