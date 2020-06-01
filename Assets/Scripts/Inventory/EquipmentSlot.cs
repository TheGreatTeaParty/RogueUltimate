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
    
    
    public void AddItemToEquipmentSlot(EquipmentItem newEquipmentItem)
    {
        _equipmentItem = newEquipmentItem; 
        icon.sprite = _equipmentItem.itemIcon; 
        icon.enabled = true;         
    }
    
    
    public void RemoveItemFromEquipmentSlot()
    {
        _equipmentItem = null; 
        icon.sprite = null;
        icon.enabled = false;
    }
    
    
    public void UnEquip()
    {
        EquipmentManager.Instance.UnEquip((int)_equipmentItem.equipmentType);
    }
    
    
    public void OpenTooltip()
    {
        PlayerPanelTooltip tooltip = PlayerPanelTooltip.Instance;
        tooltip.ShowTooltip(_equipmentItem, (int)_equipmentItem.equipmentType);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // UnEquip();
        OpenTooltip();
    }
    
    
}
