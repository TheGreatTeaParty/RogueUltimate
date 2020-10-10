using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuickSlot : ItemSlot
{
    [SerializeField] private QuickSlotOnScene quickSlotOnScene;


    private void Start()
    {
        if (quickSlotOnScene != null)
            quickSlotOnScene.OnClickEvent += UseItem;
    }

    private void UseItem()
    {
        if (_item != null)
        {
            _item.Use();
            Amount--;
        }
    }
    
}