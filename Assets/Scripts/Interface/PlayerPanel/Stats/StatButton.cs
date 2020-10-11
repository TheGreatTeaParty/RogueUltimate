using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum StatType
{
    Will = 0,
    Physique = 1,
    Mind = 2,
    Reaction = 3,
}

public class StatButton : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    public StatType statType;
    
    public event Action<StatType> onClickEvent; 
    
    
    private void Start()
    {
        _image = GetComponent<Image>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke(statType);
    }
    
}