﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform itemsParent;
    private bool _isUpdated = true;
    
    [SerializeField] private EquipmentSlot[] slots;
    [SerializeField] private EquipmentManager equipment;
    

    void Start()
    {
        equipment = EquipmentManager.instance;

        //Make delegate function from Inventory be equal to UpdateUI fun
        equipment.onEquipmentCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }
    

    //Have decited to make it with is updated in order to exlude extra caluclations in Update function
    private void Update()
    {
        if (!_isUpdated)
            UpdateUI();
    }
    
    
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (equipment.currentEquipment[i] != null)
            {
                if (!slots[i].AddItem(equipment.currentEquipment[i]))
                    _isUpdated = false;
            }
            else
            {
                if (!slots[i].ClearSlot())
                    _isUpdated = false;
            }
        }
    }
    
    
}
