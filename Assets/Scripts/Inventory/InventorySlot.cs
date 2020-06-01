﻿using System.Collections;
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
    
    
    public void OnRemoveButton()
    {
        Debug.Log("Button clicked");
       // InventoryManager.Instance.RemoveItemFromInventory(_item);
    }
    
    
    public void UseItem()
    {
        if (_item != null)
            _item.Use();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("damn raycast");
        UseItem();
    }
    
    
}
