using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Button button;
    Image icon;
    Item item;

    private void Start()
    {
        icon = GetComponent<Image>();
    }
    public bool AddItem(Item newItem)
    {
        if (icon != null)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            //Fast fix, because I have only one button in slots to test
            if(button!= null)
                button.interactable = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ClearSlot()
    {
        if (icon != null)
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            //Fast fix, because I have only one button in slots to test
            if (button != null)
                button.interactable = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }
    public void UseItem()
    {
        if (item != null)
            item.Use();
    }
}
