using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private bool _isUpdated = true;
    
    [SerializeField] private InventorySlot[] slots;
    private Inventory _inventory;


    void Start()
    {
        _inventory = Inventory.instance;
        //Make delegate function from Inventory be equal to UpdateUI fun
        _inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    
    //Have decited to make it with is updated in order to exlude extra caluclations in Update function
    private void Update()
    {
        UpdateUI();
    }
    
    
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < _inventory.items.Count)
            {
                if (!slots[i].AddItemToSlot(_inventory.items[i]))
                    _isUpdated = false;
            }
            else
            {
                if (!slots[i].RemoveItemFromSlot())
                    _isUpdated = false;
            }
        }
    }
    
    
}
