using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContractTradeIUI : MonoBehaviour
{
    public ContractSlot[] playerSlots;
    public List<TradeSlot> npcSlots;
    public Transform TradeSlot;

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

        accountManager = AccountManager.Instance;
        tavernKeeperUpgrade = TavernKeeperUpgrade.Instance;

        tradeWindow = GetComponent<ContractTradeWindow>();

        if (playerSlotsParent == null || npcSlotsParent == null)
            Debug.Log("Null pointer in TradeUI");

        npcSlots = new List<TradeSlot>();
        npcSlots.AddRange(npcSlotsParent.GetComponentsInChildren<TradeSlot>());

        tradeManager.onChangeCallback += UpdateUI;
        tradeManager.npcInventory.onItemAdded += AddTradeSlot;

        playerSlots = playerSlotsParent.GetComponentsInChildren<ContractSlot>();

        for (int i = 0; i < playerSlots.Length; i++)
            playerSlots[i].OnClick += tradeWindow.OnSlotClick;

        for (int i = 0; i < npcSlots.Count; i++)
            npcSlots[i].OnClick += tradeWindow.OnSlotClick;

        CheckSlots();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (gameObject.activeSelf == false) return;

        var i = 0;
        for (; i < tradeManager.npcInventory.items.Count; i++)
            npcSlots[i].Item = tradeManager.npcInventory.items[i];
        for (; i < npcSlots.Count; i++)
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
            }
            else
            {
                playerSlots[j].Item = null;
                ++j;
            }
            
        }
        i = 0;
        for (; i < tradeWindow.playerContracts.contracts.Count; i++)
        {
            if (tradeWindow.playerContracts.contracts[i].type == Contract.contractType.Major)
                playerSlots[3].Item = tradeWindow.playerContracts.contracts[i];
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

    private void AddTradeSlot()
    {
        var slot = Instantiate(TradeSlot, npcSlotsParent);
        TradeSlot tradeSlot = slot.GetComponent<TradeSlot>();
        tradeSlot.OnClick += tradeWindow.OnSlotClick;
        tradeSlot.Item = null;
        npcSlots.Add(tradeSlot);
    }

    public void AddTradeSlot(Item item)
    {
        if (npcSlots.Count < tradeManager.npcInventory.items.Count)
        {
            var slot = Instantiate(TradeSlot, npcSlotsParent);
            TradeSlot tradeSlot = slot.GetComponent<TradeSlot>();
            tradeSlot.OnClick += tradeWindow.OnSlotClick;
            tradeSlot.Item = item;
            npcSlots.Add(tradeSlot);

            // If we want to add by 4 Slots, if not Delete the bottom Functions: 
            AddTradeSlot();
            AddTradeSlot();
            AddTradeSlot();
        }
    }
    private void CheckSlots()
    {
        if (npcSlots.Count < tradeManager.npcInventory.items.Count)
        {
            for (int i = npcSlots.Count; i < tradeManager.npcInventory.items.Count; ++i)
            {
                var slot = Instantiate(TradeSlot, npcSlotsParent);
                TradeSlot tradeSlot = slot.GetComponent<TradeSlot>();
                tradeSlot.OnClick += tradeWindow.OnSlotClick;
                tradeSlot.Item = tradeManager.npcInventory.items[i];
                npcSlots.Add(tradeSlot);
            }
        }
    }
}
