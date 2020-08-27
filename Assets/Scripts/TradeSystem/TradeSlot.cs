using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    [SerializeField] private Image image;
    
    
    public void AddItemToTradeSlot(Item item)
    {
        if (item == null)
        {
            Debug.Log("Null reference in TradeSlot.cs");
            return;
        }
        
        _item = item;
        image.enabled = true;
        image.sprite = _item.Sprite;

    }

    public void RemoveItemFromTradeSlot()
    {
        image.sprite = null;
        image.enabled = false; 
        _item = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var tradeManager = TradeManager.Instance;
        tradeManager.currentItem = _item;
        tradeManager.DrawTooltip();

        // State: true - buy, false - sell
        tradeManager.state = gameObject.CompareTag("NPCTradeSlot");
        tradeManager.onChangeCallback();
    }
    
} 