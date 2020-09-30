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
    public CharacterManager playerCharacter;
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
        
        tradeTooltip.SetName(currentItem.ItemName);
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

       // if (playerCharacter.items.Count < playerCharacter.size && playerCharacter.GetGold() >= currentItem.Price)
        {
            playerCharacter.ChangeGold(-currentItem.Price);
            playerCharacter.AddItemToInventory(currentItem);
            AudioManager.Instance.Play("Buy");
        }
        
        onChangeCallback?.Invoke();
    }

    public void Sell()
    {
        playerCharacter.ChangeGold(currentItem.Price);
       // playerCharacter.RemoveItemFromInventory(currentItem);
       // !!!!!!!!!! QuickSlots
       
        currentItem = null;
        EraseTooltip();
        
        onChangeCallback?.Invoke();
        AudioManager.Instance.Play("Sell");
    }

    public void Bind(CharacterManager playerCharacter, NPCInventory npcInventory)
    {
        if (playerCharacter == null || npcInventory == null || tradeTooltip == null)
        {
            Debug.Log("Smth didn't found in TradeManager.cs, Bind()");
            return;
        }
        
        this.playerCharacter = playerCharacter;
        this.npcInventory = npcInventory;

        onChangeCallback?.Invoke();
    }

    public void Open()
    {
        //Return Joystick to 0 position;
        InterfaceManager.Instance.fixedJoystick.ResetInput();

        var UI = InterfaceManager.Instance;
        var playerButton = KeepOnScene.Instance.GetComponentInChildren<Button>();
        
        UI.HideAll();
        playerButton.enabled = false;
        
        tradeWindow.SetActive(true);
        
        onChangeCallback.Invoke();
        AudioManager.Instance.Play("TradeOpen");
    }

    public void Close()
    {
        var UI = InterfaceManager.Instance;
        var playerButton = KeepOnScene.Instance.GetComponentInChildren<Button>();
        
        EraseTooltip();
        
        tradeWindow.SetActive(false);
        
        UI.ShowFaceElements();
        playerButton.enabled = true;
        
        currentItem = null;
        AudioManager.Instance.Play("TradeClose");

        //No Idea How to make it normal(without null check)
        if (TavernKeeper.Instance != null)
            TavernKeeper.Instance.Talk(false);
    }

} 
