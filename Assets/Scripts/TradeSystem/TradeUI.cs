using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{
    public TradeSlot[] playerSlots;
    public TradeSlot[] npcSlots;

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

    private bool _isSet = false;

    private void Start()
    {
        tradeManager = TradeManager.Instance;
        tradeManager.onChangeCallback += UpdateUI;

        accountManager = AccountManager.Instance;
        tavernKeeperUpgrade = TavernKeeperUpgrade.Instance;

        tradeWindow = GetComponent<TradeWindow>();

        if (playerSlotsParent == null || npcSlotsParent == null) 
            Debug.Log("Null pointer in TradeUI");
        
        npcSlots = npcSlotsParent.GetComponentsInChildren<TradeSlot>();
        playerSlots = playerSlotsParent.GetComponentsInChildren<TradeSlot>();
        
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
        action.SetText(tradeWindow.currentSlot.tradeSlotType == TradeSlotType.NPC ? "Buy" : "Sell");
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
    }
} 