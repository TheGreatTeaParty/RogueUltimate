using System;
using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour
{
    private Item _item;
    private Image _image;

    
    private void Start()
    {
        _image = GetComponent<Image>();
        if (_image != null)
            _image.enabled = false;
    }

    private void AddItemToSlot(Item item)
    {
        if (item == null) return;
        _item = item;
        _image.sprite = item.Sprite;
        _image.enabled = true; // should try without it
    }

    private void DeleteItemFromSlot()
    {
        if (_item == null) return;
        _item = null;
        _image.sprite = null;
        _image.enabled = false; // should try without it
    }

    public void ShowTooltip()
    {
        var tooltip = TradeTooltip.Instance;
        
        tooltip.Show(_item);
    }
    
} 