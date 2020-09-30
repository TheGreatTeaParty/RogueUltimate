using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlot : ItemSlot, IPointerClickHandler
{
    public EquipmentType equipmentType;


    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquipmentItem equipmentItem = item as EquipmentItem;
        return equipmentItem != null && equipmentItem.equipmentType == equipmentType;
    }

}
