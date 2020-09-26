using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlot : ItemSlot, IPointerClickHandler
{
    public EquipmentType equipmentType;
    [SerializeField] private Image icon;
    [SerializeField] private EquipmentItem equipmentItem;
    
    
    public void AddItemToEquipmentSlot(EquipmentItem newEquipmentItem)
    {
        equipmentItem = newEquipmentItem; 
        icon.sprite = equipmentItem.Sprite; 
        icon.enabled = true;         
    }
    
    
    public void RemoveItemFromEquipmentSlot()
    {
        equipmentItem = null; 
        icon.sprite = null;
        icon.enabled = false;
    }


    private void OpenTooltip()
    {
        PlayerPanelTooltip tooltip = PlayerPanelTooltip.Instance;
        tooltip.ShowTooltip(equipmentItem, (int)equipmentItem.equipmentType);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (equipmentItem != null)
        {
            AudioManager.Instance.Play("Click");
            OpenTooltip();
        }
    }
    
    
}
