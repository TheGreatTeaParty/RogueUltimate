using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
                if (previousItem != null)
                    inventory.AddItem(previousItem);
                else
                    inventory.AddItem(item);
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (!inventory.CheckFullness() && equipmentPanel.RemoveItem(item))
            inventory.AddItem(item);
    }
}
    
    