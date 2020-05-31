using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlot[] slots;
    private Inventory _inventory;
    public Transform itemsParent;

    
    void Start()
    {
        _inventory = Inventory.instance;
        //Make delegate function from Inventory be equal to UpdateUI fun
        _inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < _inventory.items.Count)
            {
                slots[i].AddItemToSlot(_inventory.items[i]);
            }
            else
            {
                slots[i].RemoveItemFromSlot();
            }
        }
        //Debug.Log("Update() is working");
    }
    

}
