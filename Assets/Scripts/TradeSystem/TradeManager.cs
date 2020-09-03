using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

// Need setters and getters (?)
public class TradeManager : MonoBehaviour
{
    #region Singleton

    public static TradeManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion
    
    // State: true - buy, false - sell
    public bool state;
    public Item currentItem;
    [Space]
    public InventoryManager playerInventory;
    public NPCInventory npcInventory;
    public TradeTooltip tradeTooltip;
    public GameObject tradeWindow;


    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;


    public void Start()
    {
        tradeWindow.SetActive(true);
        EraseTooltip();
        tradeWindow.SetActive(false);
    }

    public void DrawTooltip()
    {
        if (currentItem == null)
        {
            Debug.Log("Null pointer in TradeManager.cs, ShowInfo()");
            return;
        }
        
        tradeTooltip.SetName(currentItem.Name);
        tradeTooltip.SetSprite(currentItem.Sprite);
        tradeTooltip.SetDescription(currentItem.Description);
        tradeTooltip.SetPrice(currentItem.Price);
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
        if (currentItem == null) return;
     
        // State: true - buy, false - sell
        if (state)
            Buy();
        else
            Sell();
    }
    
    public void Buy()
    {
        if (currentItem == null)
        {
            Debug.Log("Null pointer in TradeManager.cs, Buy()");
            return;
        }

        if (playerInventory.items.Count < playerInventory.size && playerInventory.GetGold() >= currentItem.Price)
        {
            playerInventory.ChangeGold(-currentItem.Price);
            playerInventory.AddItemToInventory(currentItem);
            AudioManager.Instance.Play("Buy");
        }
        
        onChangeCallback?.Invoke();
    }

    public void Sell()
    {
        playerInventory.ChangeGold(currentItem.Price);
        playerInventory.RemoveItemFromInventory(currentItem);
        if (currentItem is UsableItem)
            QuickSlotsManager.Instance.Request((UsableItem)currentItem);

        currentItem = null;
        EraseTooltip();
        
        onChangeCallback?.Invoke();
        AudioManager.Instance.Play("Sell");
    }

    public void Bind(InventoryManager playerInventory, NPCInventory npcInventory)
    {
        if (playerInventory == null || npcInventory == null || tradeTooltip == null)
        {
            Debug.Log("Smth didn't found in TradeManager.cs, Bind()");
            return;
        }
        
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;

        onChangeCallback?.Invoke();
    }

    public void Open()
    {
        //Return Joystick to 0 position;
        InterfaceOnScene.Instance.GetComponentInChildren<FixedJoystick>().ResetInput();

        var UI = InterfaceOnScene.Instance;
        var playerButton = KeepOnScene.instance.GetComponentInChildren<Button>();
        
        UI.HideAll();

        playerButton.enabled = false;
        tradeWindow.SetActive(true);
        
        onChangeCallback.Invoke();
        AudioManager.Instance.Play("TradeOpen");
    }

    public void Close()
    {
        var UI = InterfaceOnScene.Instance;
        var playerButton = KeepOnScene.instance.GetComponentInChildren<Button>();
        
        EraseTooltip();
        tradeWindow.SetActive(false);
        UI.ShowMainElements();
        playerButton.enabled = true;
        currentItem = null;
        AudioManager.Instance.Play("TradeClose");

        //No Idea How to make it normal(without null check)
        if (TavernKeeper.Instance != null)
            TavernKeeper.Instance.Talk(false);
    }

} 
