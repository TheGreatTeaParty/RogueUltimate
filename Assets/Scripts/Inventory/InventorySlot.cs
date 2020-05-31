using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    [SerializeField] private Item item;


    private void Start()
    {
        image = GetComponent<Image>();
        if (image.sprite == null)
            image.enabled = false;
    }
    
    
    public bool AddItemToSlot(Item newItem)
    {
        if (image != null)
        {
            item = newItem;
            image.sprite = item.GetSprite();
            image.enabled = true;
            return true;
        }
        
        return false;
    }
    
    
    public bool RemoveItemFromSlot()
    {
        if (image != null)
        {
            item = null;
            image.sprite = null;
            image.enabled = false;
            return true;
        }
        
        return false;
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
    }
}
