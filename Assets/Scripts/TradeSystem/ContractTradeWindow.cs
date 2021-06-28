using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractTradeWindow : MonoBehaviour
{
    public ContractHolder playerContracts;
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
        if (!playerContracts.IsFull())
        {
            playerContracts.Add(Instantiate(currentSlot.Item as Contract));
            AudioManager.Instance.Play("Buy");
            tradeManager.npcInventory.items.Remove(currentSlot.Item);
            EraseTooltip();
            currentSlot = null;
        }
        tradeManager.onChangeCallback?.Invoke();
    }

    public void Sell()
    {
        playerContracts.Remove(currentSlot.Item as Contract);
        EraseTooltip();
        tradeManager.npcInventory.items.Add(currentSlot.Item);
        currentSlot = null;
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
        gameObject.SetActive(false);
    }

    public void BindData(ContractHolder _playerContracts)
    {
        playerContracts = _playerContracts;
    }

    public void Upgrade()
    {
        tradeManager.UpgradeTradeUI(Type);
        tradeManager.onChangeCallback?.Invoke();
    }
}
