using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    private Item _item;

    private void Start()
    {
        _item = GetComponent<Item>();
        UpdateUI();
    }

    
    public void OnValidate()
    {
        UpdateUI();
    }
    

    public void UpdateUI()
    {
        if (_item == null) image.enabled = false;
        else
        {
            image.sprite = _item.itemIcon;
            image.enabled = true;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked!");
    }
}
