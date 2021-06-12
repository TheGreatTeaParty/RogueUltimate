using UnityEngine;
using UnityEngine.UI;
using System;

// Need setters and getters (?)
public class TradeManager : MonoBehaviour
{
    #region Singleton
    public enum tradeType
    {
        tavernKeeper = 0,
        smith,
        master,
        dwarf,
    };

    public static TradeManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion

    public delegate void OnChangeCallback();
    public OnChangeCallback onChangeCallback;

    public delegate void OnUpgradeCallBack<tradeType>();
    public OnUpgradeCallBack<tradeType> onUpgradeCallBack;

    [Space] 
    public Inventory playerInventory;
    public NPCInventory npcInventory;
    [Space]
    public TradeWindow KeeperWindow;
    public TradeWindow SwithWindow;
    public TradeWindow MasterWindow;
    public TradeWindow DwarfWindow;


    public event Action<tradeType> OnTradeUpgraded;

    public void Open(tradeType type)
    {
        //Return Joystick to 0 position;
        InterfaceManager.Instance.fixedJoystick.ResetInput();

        var UI = InterfaceManager.Instance;
        
        UI.HideAll();

        switch (type)
        {
            case tradeType.tavernKeeper:
                {
                    KeeperWindow.gameObject.SetActive(true);
                    KeeperWindow.BindData(playerInventory);
                    break;
                }
            default:
                break;
        }
        onChangeCallback?.Invoke();
        AudioManager.Instance.Play("TradeOpen");
    }

    public void Bind(Inventory playerInventory, NPCInventory npcInventory)
    {
        this.playerInventory = playerInventory;
        this.npcInventory = npcInventory;

        onChangeCallback?.Invoke();
    }

    public void UpgradeTradeUI(tradeType type)
    {
        OnTradeUpgraded?.Invoke(type);
        // Should be Animation Here:
    }
} 
