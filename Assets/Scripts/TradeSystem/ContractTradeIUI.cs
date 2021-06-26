using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContractTradeIUI : MonoBehaviour
{
    public ContractSlot[] playerSlots;
    public TradeSlot[] npcSlots;

    [SerializeField] private TextMeshProUGUI action;
    [SerializeField] private TextMeshProUGUI price;
    [Space]
    [SerializeField] private TradeManager tradeManager;
    [SerializeField] private Transform playerSlotsParent;
    [SerializeField] private Transform npcSlotsParent;
    public Button UButton;
    public TextMeshProUGUI LevelValue;
    [Space]
    public TextMeshProUGUI levelValue;
    public TextMeshProUGUI priceValue;
    public GameObject parent;
    public Transform template;

    private ContractTradeWindow tradeWindow;
    private AccountManager accountManager;
    private TavernKeeperUpgrade tavernKeeperUpgrade;

    private bool _isSet = false;

    private void Start()
    {
        tradeManager = TradeManager.Instance;
        tradeManager.onChangeCallback += UpdateUI;

        accountManager = AccountManager.Instance;
        tavernKeeperUpgrade = TavernKeeperUpgrade.Instance;

        tradeWindow = GetComponent<ContractTradeWindow>();

        if (playerSlotsParent == null || npcSlotsParent == null)
            Debug.Log("Null pointer in TradeUI");

        npcSlots = npcSlotsParent.GetComponentsInChildren<TradeSlot>();
        playerSlots = playerSlotsParent.GetComponentsInChildren<ContractSlot>();

        for (int i = 0; i < playerSlots.Length; i++)
            playerSlots[i].OnClick += tradeWindow.OnSlotClick;

        for (int i = 0; i < npcSlots.Length; i++)
            npcSlots[i].OnClick += tradeWindow.OnSlotClick;

        UpdateUI();
    }

    public void UpdateUI()
    {
        var i = 0;
        for (; i < tradeManager.npcInventory.items.Count; i++)
            npcSlots[i].Item = tradeManager.npcInventory.items[i];
        for (; i < npcSlots.Length; i++)
            npcSlots[i].Item = null;

        i = 0;

        int j = 0;

        for (; i < tradeWindow.playerContracts.contracts.Capacity; i++)
        {
            if (i < tradeWindow.playerContracts.contracts.Count)
            {
                if (tradeWindow.playerContracts.contracts[i].type == Contract.contractType.Regular)
                {
                    playerSlots[j].Item = tradeWindow.playerContracts.contracts[i];
                    ++j;
                }
                else
                {
                    playerSlots[3].Item = tradeWindow.playerContracts.contracts[i];
                }
            }
            else
            {
                playerSlots[j].Item = null;
            }
        }

        if (UButton)
        {
            if (accountManager.Renown >= tavernKeeperUpgrade.GetReqiredPrice(tradeWindow.Type)
                && !tavernKeeperUpgrade.IsMaxLevel(tradeWindow.Type))
                UButton.interactable = true;
            else
                UButton.interactable = false;
            LevelValue.text = ((TraitsGenerator.RenownLevel)tavernKeeperUpgrade.GetCurrentLevel(tradeWindow.Type)).ToString();
        }
       
        if (tradeWindow.currentSlot == null) return;
        action.SetText(tradeWindow.currentSlot.tradeSlotType == TradeSlotType.NPC ? "Take" : "Drop");
        price.SetText(tradeWindow.currentSlot.Item.Price.ToString());
    }

    public void SetUpUpgradeData()
    {
        if (!_isSet)
        {
            levelValue.text = ((TraitsGenerator.RenownLevel)(tavernKeeperUpgrade.GetCurrentLevel(tradeWindow.Type) + 1)).ToString();
            priceValue.text = tavernKeeperUpgrade.GetReqiredPrice(tradeWindow.Type).ToString();
            GetUnlock();
        }
        _isSet = true;
    }
    private void GetUnlock()
    {
        
    }
    public void UpgradeButton()
    {
        _isSet = false;
    }
}
