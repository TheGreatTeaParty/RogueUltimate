﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlot : ItemSlot, IPointerClickHandler
{
    [SerializeField] private Image shadowedIcon;
    [SerializeField] private EquipmentType equipmentType;
    
    public EquipmentType EquipmentType
    {
        get => equipmentType;
        set => equipmentType = value;
    }


    protected override void Awake()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        for (int i = 1; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                image = images[i];
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquipmentItem equipmentItem = item as EquipmentItem;
        return equipmentItem != null && equipmentItem.EquipmentType == equipmentType;
    }
    
}
