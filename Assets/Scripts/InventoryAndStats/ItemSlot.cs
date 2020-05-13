using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    [SerializeField] Image image; 
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
        if (eventData != null && eventData.clickCount > 0)
            if (Item != null && onTouchEvent != null)
                onTouchEvent(_item);
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
    
}
