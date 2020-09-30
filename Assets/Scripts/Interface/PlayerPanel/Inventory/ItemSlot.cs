using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected Text amountText;
  
    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);
    
    public Item _item;
    public Item Item
    {
        get => _item;
        set {
            _item = value;
            if (_item == null && Amount != 0) Amount = 0;

            if (_item == null) 
            {
                image.sprite = null;
                image.color = disabledColor;
            } 
            else 
            {
                image.sprite = _item.Sprite;
                image.color = normalColor;
            }
            
        }
    }

    private int _amount;
    public int Amount
    {
        get => _amount;
        set {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0 && Item != null) Item = null;

            if (amountText != null)
            {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled) 
                    amountText.text = _amount.ToString();
            }
        }
        
    }
    
    
    public event Action<ItemSlot> OnClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    
    
    public virtual bool CanAddStack(Item item, int amount = 1)
    {
        return Item != null && Item.ID == item.ID && Amount + amount <= item.StackMaxSize;
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickEvent == null) return;
        
        OnClickEvent.Invoke(this);
        AudioManager.Instance.Play("Click");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }
    
}
