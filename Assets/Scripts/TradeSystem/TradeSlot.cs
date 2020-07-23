using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    private Image _image;

    
    public void AddItemToTradeSlot(Item item)
    {
        if (item == null)
        {
            Debug.Log("Null reference in TradeSlot.cs");
            return;
        }

        if (_image != null)
        {
            _item = item;
            _image.sprite = _item.Sprite;
            _image.enabled = true;
        }
    }

    public void RemoveItemFromTradeSlot()
    {
        if (_image != null)
        {
            _image.sprite = null;
            _image.enabled = false;
            _item = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var tradeManager = TradeManager.Instance;
        tradeManager.currentItem = _item;
        tradeManager.OpenTooltip();

        // true - buy, false - sell
        if (gameObject.CompareTag("NPCTradeSlot"))
            tradeManager.SetState(true);
        else tradeManager.SetState(false);

    }
    
} 