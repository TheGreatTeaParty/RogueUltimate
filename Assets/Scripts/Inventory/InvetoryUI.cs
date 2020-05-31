using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlot[] slots;
    private Inventory _inventory;
    private bool _isUpdated;
    public Transform itemsParent;

    
    void Start()
    {
        _inventory = Inventory.instance;
        _isUpdated = false;
        //Make delegate function from Inventory be equal to UpdateUI fun
        _inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }


    private void Update()
    {
        if (!_isUpdated)
            UpdateUI();
    }


    void UpdateUI()
    {
        int i = 0;
        for (; i < _inventory.items.Count; i++)
            if (slots[i].AddItemToSlot(_inventory.items[i])) _isUpdated = slots[i].image.enabled;
        
        for (; i < slots.Length; i++)
            if (slots[i].RemoveItemFromSlot()) _isUpdated = slots[i].image.enabled;
        //Debug.Log("Update() is working");
    }
    

}
