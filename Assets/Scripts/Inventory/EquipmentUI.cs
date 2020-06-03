using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] equipmentSlots;
    [SerializeField] private EquipmentManager equipmentManager;
    public Transform itemsParent;   

    
    void Start()
    {
        equipmentManager = EquipmentManager.Instance;
        //Make delegate function from InventoryManager be equal to UpdateUI fun
        equipmentManager.onEquipmentCallback += UpdateUI;
        equipmentSlots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }
    

    private void UpdateUI()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentManager.currentEquipment[i] != null) equipmentSlots[i].AddItemToEquipmentSlot(equipmentManager.currentEquipment[i]);
            else equipmentSlots[i].RemoveItemFromEquipmentSlot();
        }
    }
    
    
}
