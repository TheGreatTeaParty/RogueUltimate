using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    public Transform itemsParent;

    InventorySlot[] slots;
    Inventory inventory;
    bool is_updated = true;
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
        if (!is_updated)
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
                    is_updated = false;
            }
            else
            {
                if (!slots[i].ClearSlot())
                    is_updated = false;
            }
        }
    }
}
