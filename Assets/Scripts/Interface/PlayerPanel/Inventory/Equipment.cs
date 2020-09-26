using System;
using UnityEngine;


public class Equipment : MonoBehaviour
{
    public Transform transformparent;
    public EquipmentSlot[] equipmentSlots;
    
    
    public event Action<ItemSlot> OnClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnClickEvent += OnClickEvent;
            equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipmentSlots[i].OnDragEvent += OnDragEvent;
            equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    public bool AddItem(EquipmentItem item, out EquipmentItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquipmentItem) equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                return true;
            }
        }

        previousItem = null;
        return false;
    }
    
}