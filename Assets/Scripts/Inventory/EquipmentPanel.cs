using System;
using UnityEngine;


public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private EquipmentSlot[] equipmentSlots;
    public event Action<Item> OnItemTouchedEvent;
    
    
    public void Start()
    {
        foreach (var equipmentSlot in equipmentSlots)
            equipmentSlot.OnTouchEvent += OnItemTouchedEvent;
    }


    private void OnValidate()
    {
        equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
    }
    

    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        foreach (var equipmentSlot in equipmentSlots)
            if (equipmentSlot.equipmentType == item.equipmentType)
            {
                previousItem = (EquippableItem)equipmentSlot.Item;
                equipmentSlot.Item = item;
                return true;
            }

        previousItem = null;
        return false;
    }
    
    
    public bool RemoveItem(EquippableItem item)
    {
        foreach (var equipmentSlot in equipmentSlots)
            if (equipmentSlot.Item == item)
            {
                equipmentSlot.Item = null;
                return true;
            }

        return false;
    }
    
}
