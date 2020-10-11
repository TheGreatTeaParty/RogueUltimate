using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    protected Item _item;
    protected int _amount;
    protected bool _tooltipIsOpened = false;
    
    protected Image image;
    protected TMP_Text amountText;
  
    protected Color normalColor = Color.white;
    protected Color shadowColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    protected Color disabledColor = Color.clear;
    

    public Item Item
    {
        get => _item;
        set 
        {
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
    public int Amount
    {
        get => _amount;
        set 
        {
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
    public bool TooltipIsOpened
    {
        get => _tooltipIsOpened;
        set => _tooltipIsOpened = value;
    }


    public event Action<ItemSlot> OnClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    protected virtual void Awake()
    {
        var images = gameObject.GetComponentsInChildren<Image>();
        var texts = gameObject.GetComponentsInChildren<TMP_Text>();
        
        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                image = images[i];
        
        for (int i = 0; i < texts.Length; i++)
            if (texts[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                amountText = texts[i];
    }
    
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
        if (_item == null) return;
        
        OnClickEvent?.Invoke(this);
        AudioManager.Instance.Play("Click");
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (_item == null) return;
        
        image.color = shadowColor;
        OnBeginDragEvent?.Invoke(this);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this);
        if (_item != null)
            image.color = normalColor;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }
    
}
