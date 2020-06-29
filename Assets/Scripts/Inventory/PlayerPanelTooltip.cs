using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPanelTooltip : MonoBehaviour
{
    #region Singleton
    public static PlayerPanelTooltip Instance;
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    #endregion
    
    private int _index;
    [SerializeField] private Item item;
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI optionalButtonText;
    [Space]
    [SerializeField] private GameObject dropButton;
    [SerializeField] private GameObject optionalButton;
    [SerializeField] private GameObject quickAccessButton;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    
    public void ShowTooltip(Item pureItem)
    {
        gameObject.SetActive(false);
        
        item = pureItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        optionalButton.SetActive(false);
        quickAccessButton.SetActive(false);
        
        gameObject.SetActive(true);
    }
    
    public void ShowTooltip(EquipmentItem equipmentItem)
    {
        gameObject.SetActive(false);
        
        item = equipmentItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;

        quickAccessButton.SetActive(false);
        optionalButton.SetActive(true);
        optionalButtonText.text = "Equip";
        
        gameObject.SetActive(true);
    }

    public void ShowTooltip(EquipmentItem equipmentItem, int slotIndex)
    {
        gameObject.SetActive(false);
        
        item = equipmentItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        _index = slotIndex;

        quickAccessButton.SetActive(false);
        optionalButton.SetActive(true);
        optionalButtonText.text = "Unequip";
        
        gameObject.SetActive(true);
    }
    
    public void ShowTooltip(UsableItem usableItem)
    {
        gameObject.SetActive(false);
        item = usableItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;

        quickAccessButton.SetActive(true);
        optionalButton.SetActive(true);
        optionalButtonText.text = "Use";
        
        gameObject.SetActive(true);
    }


    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
    
    private void DropButtonPress()
    {
        item.Drop();
        HideTooltip();
    }


    private void OptionalButtonPress()
    {
        if (optionalButtonText.text == "Equip" || optionalButtonText.text == "Use") 
            item.Use();
        else if (optionalButtonText.text == "Unequip") 
            EquipmentManager.Instance.Unequip(_index);

        HideTooltip();
    }


    public void QuickButtonPress()
    {
        item.MoveToQuickAccess();
        HideTooltip();
    }
    
    
    
}
