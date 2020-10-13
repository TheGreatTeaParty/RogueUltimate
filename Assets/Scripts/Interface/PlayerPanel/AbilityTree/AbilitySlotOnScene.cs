using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AbilitySlotOnScene : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private Slider _slider;
    private float _cooldownTime = 0;
    

    public event Action<AbilitySlotOnScene> OnClickEvent;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _slider = GetComponent<Slider>();
        
        _image.color = Color.clear;
    }

    private void Update()
    {
        if (_cooldownTime <= 0) return;
        
        _cooldownTime -= Time.deltaTime;
        _slider.value -= Time.deltaTime;
    }

    public void WakeCooldown(float time)
    {
        _cooldownTime = time;
        _slider.maxValue = _cooldownTime;
        _slider.value = _cooldownTime;
    }

    public void SetAbility(AbilityQuickSlot abilityQuickSlot)
    {
        if (abilityQuickSlot.Ability != null)
        {
            _image.sprite = abilityQuickSlot.Ability.Sprite;
            _image.color = Color.white;
            return;
        }

        _image.sprite = null;
        _image.color = Color.clear;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cooldownTime <= 0 && _image.sprite != null)
            OnClickEvent?.Invoke(this);
    }
    
}