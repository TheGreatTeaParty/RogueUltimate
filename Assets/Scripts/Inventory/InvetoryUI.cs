using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private bool _isUpdated = true;
    
    [SerializeField] private InventorySlot[] slots;
    [SerializeField] private Inventory inventory;


    void Start()
    {
        inventory = Inventory.instance;

        //Make delegate function from Inventory be equal to UpdateUI fun
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    
    //Have decited to make it with is updated in order to exlude extra caluclations in Update function
    private void Update()
    {
        if (!_isUpdated)
        {
            UpdateUI();
        }
    }
    
    
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                if (!slots[i].AddItem(inventory.items[i]))
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
