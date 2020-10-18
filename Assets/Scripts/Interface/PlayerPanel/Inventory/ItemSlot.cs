using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    protected Item item;
    private int _amount;
    private bool _tooltipIsOpened = false;
    
    protected Image image;
    private TMP_Text _amountText;

    private readonly Color _normalColor = Color.white;
    private readonly Color _shadowColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    private readonly Color _disabledColor = Color.clear;
    

    public Item Item
    {
        get => item;
        set 
        {
            item = value;
            if (item == null && Amount != 0) Amount = 0;

            if (item == null) 
            {
                image.sprite = null;
                image.color = _disabledColor;
            } 
            else 
            {
                image.sprite = item.Sprite;
                image.color = _normalColor;
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

            if (_amountText != null)
            {
                _amountText.enabled = item != null && _amount > 1;
                if (_amountText.enabled) 
                    _amountText.text = _amount.ToString();
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
        _amountText = gameObject.GetComponentInChildren<TMP_Text>();
        
        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                image = images[i];
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
        if (item == null) return;
        
        OnClickEvent?.Invoke(this);
        AudioManager.Instance.Play("Click");
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) return;
        
        image.color = _shadowColor;
        OnBeginDragEvent?.Invoke(this);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this);
        if (item != null)
            image.color = _normalColor;
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
