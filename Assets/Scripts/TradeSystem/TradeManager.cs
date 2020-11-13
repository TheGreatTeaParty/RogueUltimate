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
    
    public TradeSlot currentSlot;
    [Space] 
    public Inventory playerInventory;
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

    public void OnSlotClick(TradeSlot tradeSlot)
    {
        currentSlot = tradeSlot;
        DrawTooltip();
        AudioManager.Instance.Play("Click");
        
        onChangeCallback();
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
        
        onChangeCallback?.Invoke();
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
        
        
        onChangeCallback?.Invoke();
        AudioManager.Instance.Play("Sell");
    }

    public void Bind(Inventory playerInventory, NPCInventory npcInventory)
    {
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;

        onChangeCallback?.Invoke();
    }

    public void Open()
    {
        //Return Joystick to 0 position;
        InterfaceManager.Instance.fixedJoystick.ResetInput();

        var UI = InterfaceManager.Instance;
        
        UI.HideAll();
        
        tradeWindow.SetActive(true);
        
        onChangeCallback.Invoke();
        AudioManager.Instance.Play("TradeOpen");
    }

    public void Close()
    {
        var UI = InterfaceManager.Instance;
        var playerButton = PlayerOnScene.Instance.GetComponentInChildren<Button>();
        
        EraseTooltip();
        
        tradeWindow.SetActive(false);
        
        UI.ShowFaceElements();
        
        currentSlot = null;
        AudioManager.Instance.Play("TradeClose");

        //No Idea How to make it normal(without null check)
        if (TavernKeeper.Instance != null)
            TavernKeeper.Instance.Talk(false);
    }

} 
