using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuickSlot : ItemSlot
{
    [SerializeField] private QuickSlotOnScene quickSlotOnScene;

    public event Action<QuickSlot> OnQuickDropEvent;
    

    private void Start()
    {
        if (quickSlotOnScene != null)
            quickSlotOnScene.OnClickEvent += UseItem;
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