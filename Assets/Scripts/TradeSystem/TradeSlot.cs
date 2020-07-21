using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    private Image _image;

    public Image Image => _image;
    

    public void AddItemToTradeSlot(Item item)
    {
        if (item == null)
        {
            Debug.Log("Null reference in TradeSlot.cs");
            return;
        }
        
        _item = item;
        _image.sprite = _item.Sprite;
        _image.enabled = true;
    }

    public void RemoveItemFromTradeSlot()
    {
        _image.sprite = null;
        _image.enabled = false;
        _item = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TradeManager.Instance.currentItem = _item;
        TradeManager.Instance.OpenTooltip();
    }
    
} 