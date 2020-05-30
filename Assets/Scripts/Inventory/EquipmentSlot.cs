using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private EquipmentItem _equipmentItem;

    
    private void Start()
    {
        icon = GetComponent<Image>();
    }
    
    
    public bool AddItem(EquipmentItem newEquipmentItem)
    {
        if (icon != null)
        {
            _equipmentItem = newEquipmentItem;
            icon.sprite = _equipmentItem.itemIcon;
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
            _equipmentItem = null;
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
        EquipmentManager.instance.UnEquip((int)_equipmentItem.equipmentType);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
       //
    }
    
    
}