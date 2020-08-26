using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    private Image _image;

    
    public void Awake()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            Debug.Log("Null Reference in TradeSlot");
            return;
        }
    }
    
    public void AddItemToTradeSlot(Item item)
    {
        if (item == null)
        {
            Debug.Log("Null reference in TradeSlot.cs");
            return;
        }
        
        if (_image == null) Debug.Log("There");
        _item = item;
        _image.enabled = true;
        _image.sprite = _item.Sprite;

    }

    public void RemoveItemFromTradeSlot()
    {
        _image.sprite = null;
        _image.enabled = false; 
        _item = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var tradeManager = TradeManager.Instance;
        tradeManager.currentItem = _item;
        tradeManager.DrawTooltip();

        // State: true - buy, false - sell
        tradeManager.state = gameObject.CompareTag("NPCTradeSlot");
    }
    
} 