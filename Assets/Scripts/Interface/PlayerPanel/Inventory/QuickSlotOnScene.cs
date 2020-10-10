using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotOnScene : MonoBehaviour, IPointerClickHandler
{
    private Image _image;

    public event Action OnClickEvent;


    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.clear;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();
    }
    
}