using System;
using UnityEngine;


public class Equipment : MonoBehaviour
{
    public Transform transformParent;
    public EquipmentSlot[] equipmentSlots;


    public event Action<ItemSlot> OnClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    

    private void Awake()
    {
        equipmentSlots = transformParent.GetComponentsInChildren<EquipmentSlot>();
    }
    
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
            if (equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                if (!equipmentSlots[i].Item)
                    equipmentSlots[i].Amount++;

                previousItem = (EquipmentItem) equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                equipmentSlots[i].SetTier(item);
                
                return true;
            }
        }

        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquipmentItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        
        return false;
    }

    public EquipmentItem GetWeaponItem()
    {
        return equipmentSlots[5].Item as EquipmentItem;
    }

}