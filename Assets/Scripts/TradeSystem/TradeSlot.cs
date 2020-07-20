using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour
{
    private Item _item;
    private Image _image;
    
    public Image Image => _image;


    public void AddItemToTradeSlot(Item item)
    {
        _item = item;
    }

    public void RemoveItemFromTradeSlot()
    {
        _item = null;
    }

} 