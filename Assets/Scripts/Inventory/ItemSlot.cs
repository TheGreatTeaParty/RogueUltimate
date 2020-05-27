using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    [SerializeField] private Image image;
    public event Action<Item> OnTouchEvent; 
    
    
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            if (_item == null)
                image.enabled = false;
            else
            {
                image.sprite = _item.icon;
                image.enabled = true;
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    { 
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left) 
            if (Item != null && OnTouchEvent != null)
                OnTouchEvent(_item);
    }


    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
    
}
