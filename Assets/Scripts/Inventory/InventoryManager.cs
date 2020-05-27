using System;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentPanel equipmentPanel;
    [SerializeField] private GameObject panel;

    
    private void Awake()
    {
        inventory.OnItemTouchedEvent += EquipFromInventory;
        equipmentPanel.OnItemTouchedEvent += UnequipFromEquipmentPanel;
        panel.SetActive(false);
    }

    
    public void EquipFromInventory(Item item)
    {
        if (item is EquippableItem) 
            Equip((EquippableItem)item);
    }


    public void UnequipFromEquipmentPanel(Item item)
    {
        if (item is EquippableItem) 
            Unequip((EquippableItem)item);
    }


    private void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                    inventory.AddItem(previousItem);
            }
            else inventory.AddItem(item);
        }
    }

    
    public void Unequip(EquippableItem item)
    {
        if (!inventory.CheckFullness() && equipmentPanel.RemoveItem(item))
            inventory.AddItem(item);
    }
}
    