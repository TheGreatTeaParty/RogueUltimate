using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum TradeSlotType
{
    Player = 0,
    NPC = 1,
}


public class TradeSlot : MonoBehaviour, IPointerClickHandler
{
    public TradeSlotType tradeSlotType;
    public event Action<TradeSlot> OnClick;
    protected Item item;
    private int _amount;
    private bool _tooltipIsOpened = false;

    protected Image image;
    private TMP_Text _amountText;

    private readonly Color _normalColor = Color.white;
    private readonly Color _shadowColor = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    private readonly Color _disabledColor = Color.clear;


    public virtual Item Item
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

    protected virtual void Awake()
    {
        var images = gameObject.GetComponentsInChildren<Image>();
        _amountText = gameObject.GetComponentInChildren<TMP_Text>();

        for (int i = 0; i < images.Length; i++)
            if (images[i].gameObject.transform.parent.GetInstanceID() != GetInstanceID())
                image = images[i];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item != null)
            OnClick?.Invoke(this);
    }
} 
