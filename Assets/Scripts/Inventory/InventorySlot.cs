using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    private Item _item;


    public void Start()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            Debug.Log("Null Reference in InventorySlot");
            return;
        }
        
    }
    
    public void AddItemToInventorySlot(Item newItem)
    {
        _item = newItem;
        _image.enabled = true;
        _image.sprite = _item.Sprite;
    }
    
    
    public void RemoveItemFromInventorySlot()
    {
        _item = null; 
        _image.sprite = null; 
        _image.enabled = false;
    }


    private void ShowTooltip()
    {
        var tooltip = PlayerPanelTooltip.Instance;
        
        if (_item is EquipmentItem) 
            tooltip.ShowTooltip((EquipmentItem)_item);
        else if (_item is UsableItem) 
            tooltip.ShowTooltip((UsableItem)_item);
        else 
            tooltip.ShowTooltip(_item);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null)
            ShowTooltip();
    }
    
    
}
