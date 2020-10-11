using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum TradeSlotType
{
    Player = 0,
    NPC = 1,
}


public class TradeSlot : ItemSlot, IPointerClickHandler
{
    public TradeSlotType tradeSlotType;
    public event Action<TradeSlot> OnClick;
    
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (Item != null)
            OnClick?.Invoke(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        
    }
} 
