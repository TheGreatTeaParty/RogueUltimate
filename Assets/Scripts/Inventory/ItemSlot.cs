using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    [SerializeField] private Image image;
    public event Action<Item> onTouchEvent; 
    
    
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
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right) 
            onTouchEvent(_item);
    }


    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
    
}
