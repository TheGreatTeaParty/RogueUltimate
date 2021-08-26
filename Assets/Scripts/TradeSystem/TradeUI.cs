using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TradeUI : MonoBehaviour
{
    public TradeSlot[] playerSlots;
    public List <TradeSlot> npcSlots;
    public Transform TradeSlot;

    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI relation;
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

    private TradeWindow tradeWindow;
    private AccountManager accountManager;
    private TavernKeeperUpgrade tavernKeeperUpgrade;
    private AudioManager audioManager;

    private bool _isSet = false;

    private void Start()
    {
        tradeManager = TradeManager.Instance;
        tradeManager.onChangeCallback += UpdateUI;
        accountManager = AccountManager.Instance;
        tavernKeeperUpgrade = TavernKeeperUpgrade.Instance;
        audioManager = AudioManager.Instance;

        tradeWindow = GetComponent<TradeWindow>();

        if (playerSlotsParent == null || npcSlotsParent == null) 
            Debug.Log("Null pointer in TradeUI");
        npcSlots = new List<TradeSlot>();
        npcSlots.AddRange(npcSlotsParent.GetComponentsInChildren<TradeSlot>());
        tradeManager.npcInventory.onItemAdded += AddTradeSlot;

        playerSlots = playerSlotsParent.GetComponentsInChildren<TradeSlot>();
        
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
        for (; i < tradeManager.playerInventory.GetInventoryCapacity(); i++)
        {
            if (tradeManager.playerInventory.ItemSlots[i])
            {
                playerSlots[i].Item = tradeManager.playerInventory.ItemSlots[i].Item;
                playerSlots[i].Amount = tradeManager.playerInventory.ItemSlots[i].Amount;
            }
            else
            {
                playerSlots[i].Item = null;
                playerSlots[i].Amount = 0;
            }
        }
        
        for(;i < playerSlots.Length; ++i)
        {
            playerSlots[i].gameObject.SetActive(false);
        }

        if (UButton)
        {
            if (accountManager.Renown >= tavernKeeperUpgrade.GetReqiredPrice(tradeWindow.Type)
                && !tavernKeeperUpgrade.IsMaxLevel(tradeWindow.Type))
                UButton.interactable = true;
            else
                UButton.interactable = false;
            LevelValue.text = tavernKeeperUpgrade.GetCurrentLevel(tradeWindow.Type).ToString();
        }

        tradeManager.playerInventory.UpdateGold();
        gold.SetText(tradeManager.playerInventory.Gold.ToString());
        relation.SetText(tradeManager.npcInventory.GetRelation().ToString());
        
        if (tradeWindow.currentSlot == null) return;
        action.SetText(tradeWindow.currentSlot.tradeSlotType == TradeSlotType.NPC ? LocalizationSystem.GetLocalisedValue("trade_buy") : LocalizationSystem.GetLocalisedValue("trade_sell"));
        price.SetText(tradeWindow.currentSlot.Item.Price.ToString());
    }

    public void SetUpUpgradeData()
    {
        if (!_isSet)
        {
            levelValue.text = (tavernKeeperUpgrade.GetCurrentLevel(tradeWindow.Type)+1).ToString();
            priceValue.text = tavernKeeperUpgrade.GetReqiredPrice(tradeWindow.Type).ToString();
            GetUnlock();
        }
        _isSet = true;
    }
    private void GetUnlock()
    {
        switch (tradeWindow.Type)
        {
            case TradeManager.tradeType.tavernKeeper:
                {
                    var unlock = tradeManager.npcInventory.gameObject.GetComponent<TavernKeeper>().GetList();
                    for(int i = 0; i < unlock.Count; ++i)
                    {
                        var temp = Instantiate(template, parent.transform);
                        temp.GetComponentsInChildren<Image>()[1].sprite = unlock[i].Sprite;
                    }
                    break;
                }
            case TradeManager.tradeType.smith:
                {
                    var unlock = tradeManager.npcInventory.gameObject.GetComponent<Smith>().GetList();
                    for (int i = 0; i < unlock.Count; ++i)
                    {
                        var temp = Instantiate(template, parent.transform);
                        temp.GetComponentsInChildren<Image>()[1].sprite = unlock[i].Sprite;
                    }
                    break;
                }
            case TradeManager.tradeType.master:
                {

                    break;
                }
        }
    }
    public void UpgradeButton()
    {
        _isSet = false;
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        UpdateUI();
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
            var rect = npcSlotsParent.GetComponent<RectTransform>().rect.height;
            npcSlotsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(360, rect + 80);
            npcSlotsParent.GetComponent<GridLayoutGroup>().padding.top = 15;
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
            for(int i = npcSlots.Count; i < tradeManager.npcInventory.items.Count; ++i)
            {
                AddTradeSlot(tradeManager.npcInventory.items[i]);
            }
        }
    }
} 