using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    Image icon;
    Equipment equipment;

    private void Start()
    {
        icon = GetComponent<Image>();
    }
    public bool AddItem(Equipment newEquipment)
    {
        if (icon != null)
        {
            equipment = newEquipment;
            icon.sprite = equipment.itemIcon;
            icon.enabled = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ClearSlot()
    {
        if (icon != null)
        {
            equipment = null;
            icon.sprite = null;
            icon.enabled = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UnEquip()
    {
        EquipmentManager.instance.UnEquip((int)equipment.equipmentType);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       //
    }
}