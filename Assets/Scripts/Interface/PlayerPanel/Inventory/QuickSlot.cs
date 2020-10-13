using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuickSlot : ItemSlot
{
    public QuickSlotOnScene quickSlotOnScene;

    public event Action<QuickSlot> OnQuickDropEvent;
    

    private void Start()
    {
        quickSlotOnScene.OnClickEvent += UseItem;

        if (Item != null)
            quickSlotOnScene.SetItem(this);
    }

    private void UseItem(QuickSlotOnScene quickSlotOnScene)
    {
        if (_item == null) return;
        
        _item.Use();
        Amount--;
        
        quickSlotOnScene.SetItem(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        OnQuickDropEvent?.Invoke(this);
        quickSlotOnScene.SetItem(this);
    }

}