using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Button _button;
    private Image _image;
    [SerializeField] private Item item;

    
    private void Start()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }
    
    
    public bool AddItemToSlot(Item newItem)
    {
        if (_image != null)
        {
            item = newItem;
            _image.sprite = item.itemIcon;
            _image.enabled = true;
            //Fast fix, because I have only one _button in slots to test
            if(_button != null)
                _button.interactable = true;
            
            return true;
        }
        
        return false;
    }
    
    
    public bool RemoveItemFromSlot()
    {
        if (_image != null)
        {
            item = null;
            _image.sprite = null;
            _image.enabled = false;
            //Fast fix, because I have only one _button in slots to test
            if (_button != null)
                _button.interactable = false;
            return true;
        }
        
        return false;
    }
    
    
    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItemFromInventory(item);
    }
    
    
    public void UseItem()
    {
        if (item != null)
            item.Use();
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
    }
    
    
}
