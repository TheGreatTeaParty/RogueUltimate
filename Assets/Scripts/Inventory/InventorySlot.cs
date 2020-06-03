using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    private Item _item;
    
    
    public void AddItemToInventorySlot(Item newItem)
    {
        _item = newItem;
        image.sprite = _item.GetSprite();
        image.enabled = true;
    }
    
    
    public void RemoveItemFromInventorySlot()
    {
        _item = null; 
        image.sprite = null; 
        image.enabled = false;
    }


    public void UseItem()
    {
        if (_item != null)
            _item.Use();
    }


    private void ShowTooltip()
    {
        PlayerPanelTooltip tooltip = PlayerPanelTooltip.Instance;
        if (_item is EquipmentItem) tooltip.ShowTooltip((EquipmentItem)_item);
        else tooltip.ShowTooltip(_item);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null)
            ShowTooltip();
    }
    
    
}
