using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    private Item item;
    
    public void AddItemToSlot(Item newItem)
    {
            item = newItem;
            image.sprite = item.GetSprite();
            image.enabled = true;
    }
    
    
    public void RemoveItemFromSlot()
    {
            item = null;
            image.sprite = null;
            image.enabled = false;
    }
    
    
    public void OnRemoveButton()
    {
        Debug.Log("Button clicked");
       // Inventory.instance.RemoveItemFromInventory(item);
    }
    
    
    public void UseItem()
    {
        if (item != null)
            item.Use();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("damn raycast");
        UseItem();
    }
}
