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

    private TradeWindow tradeWindow;
    private AccountManager accountManager;
    private TavernKeeperUpgrade tavernKeeperUpgrade;

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

} 