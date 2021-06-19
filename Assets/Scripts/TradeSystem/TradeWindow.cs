﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeWindow : MonoBehaviour
{
    private Inventory playerInventory;
    private TradeManager tradeManager;

    public TradeManager.tradeType Type;

    public TradeTooltip tradeTooltip;
    public TradeSlot currentSlot;


    public void Start()
    {
        tradeManager = TradeManager.Instance;
        EraseTooltip();
    }

    public void OnSlotClick(TradeSlot tradeSlot)
    {
        currentSlot = tradeSlot;
        DrawTooltip();
        AudioManager.Instance.Play("Click");

        tradeManager.onChangeCallback();
    }

    public void DrawTooltip()
    {
        tradeTooltip.SetName(currentSlot.Item.ItemName);
        tradeTooltip.SetSprite(currentSlot.Item.Sprite);
        tradeTooltip.SetDescription(currentSlot.Item.Description);
        tradeTooltip.SetPrice(currentSlot.Item.Price);
    }

    public void EraseTooltip()
    {
        tradeTooltip.SetName("");
        tradeTooltip.SetDescription("");
        tradeTooltip.SetSprite(null);
        tradeTooltip.SetPrice(-1);
    }

    public void Trade()
    {
        if (currentSlot == null) return;

        // state: true - buy, false - sell
        if (currentSlot.tradeSlotType == TradeSlotType.NPC)
            Buy();
        else
            Sell();
    }

    public void Buy()
    {
        if (!playerInventory.IsFull() && playerInventory.Gold >= currentSlot.Item.Price)
        {
            playerInventory.Gold -= currentSlot.Item.Price;
            playerInventory.AddItem(currentSlot.Item);
            AudioManager.Instance.Play("Buy");
        }

        tradeManager.onChangeCallback?.Invoke();
    }

    public void Sell()
    {
        playerInventory.Gold += currentSlot.Item.Price;
        playerInventory.RemoveItem(currentSlot.Item);

        currentSlot.Amount--;
        if (currentSlot.Amount < 1)
        {
            EraseTooltip();
            currentSlot = null;
        }


        tradeManager.onChangeCallback?.Invoke();
        AudioManager.Instance.Play("Sell");
    }

    public void Close()
    {
        var UI = InterfaceManager.Instance;

        EraseTooltip();


        UI.ShowFaceElements();

        currentSlot = null;
        AudioManager.Instance.Play("TradeClose");

        //No Idea How to make it normal(without null check)
        if (TavernKeeper.Instance != null)
            TavernKeeper.Instance.Talk(false);
        gameObject.SetActive(false);
    }

    public void BindData(Inventory _playerInventory)
    {
        playerInventory = _playerInventory;
    }

    public void Upgrade()
    {
        tradeManager.UpgradeTradeUI(Type);
        tradeManager.onChangeCallback?.Invoke();
    }
}