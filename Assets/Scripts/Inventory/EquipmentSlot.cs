﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private EquipmentItem equipmentItem;

    
    private void Start()
    {
        icon = GetComponent<Image>();
    }
    
    
    public void AddItemToEquipmentSlot(EquipmentItem newEquipmentItem)
    {
        equipmentItem = newEquipmentItem; 
        icon.sprite = equipmentItem.itemIcon; 
        icon.enabled = true;         
    }
    
    
    public void RemoveItemFromEquipmentSlot()
    {
        equipmentItem = null; 
        icon.sprite = null;
        icon.enabled = false;
    }


    private void OpenTooltip()
    {
        PlayerPanelTooltip tooltip = PlayerPanelTooltip.Instance;
        tooltip.ShowTooltip(equipmentItem, (int)equipmentItem.equipmentType);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (equipmentItem != null)
            OpenTooltip();
    }
    
    
}
