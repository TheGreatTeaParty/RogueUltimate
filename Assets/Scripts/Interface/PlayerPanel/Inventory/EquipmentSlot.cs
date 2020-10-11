using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlot : ItemSlot, IPointerClickHandler
{
    public EquipmentType equipmentType;


    protected override void Awake()
    {
        Image[] images = gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                image = images[i];    
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquipmentItem equipmentItem = item as EquipmentItem;
        return equipmentItem != null && equipmentItem.equipmentType == equipmentType;
    }

}
