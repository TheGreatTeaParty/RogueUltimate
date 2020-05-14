using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


public class ItemSlot : MonoBehaviour
{
    private Item _item;
    [SerializeField] Button button; 
    public event Action<Item> onTouchEvent; 
    
    
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            if (_item == null)
                button.image.enabled = false;
            else
            {
                button.image.sprite = _item.icon;
                button.image.enabled = true;
            }
        }
    }


    public void Start()
    {
        button = GetComponent<Button>();
        //button.onClick.AddListener(Click);
    }

    
    public void Click()
    { 
        if (Item != null && onTouchEvent != null) 
            onTouchEvent(_item);
    }


    protected virtual void OnValidate()
    {
        if (button.image == null)
            button.image = GetComponent<Image>();
    }
    
}
