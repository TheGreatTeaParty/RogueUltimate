using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour, IDragHandler
{
    private Item _item;
    private ItemSlot _itemSlot;
    // UI
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Button optionalButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button dropButton;
    [SerializeField] private Image effectImage;
    [Space]
    [SerializeField] private GameObject _req;
    [SerializeField] private TextMeshProUGUI _strength;
    [SerializeField] private TextMeshProUGUI _agility;
    [SerializeField] private TextMeshProUGUI _int;


    public event Action<Item> DropItem;  
    public event Action<Tooltip> CloseTooltip;


    private void Start()
    {
        CloseTooltip += CharacterManager.Instance.RemoveTooltip;
        DropItem += CharacterManager.Instance.DropFromInventory;
    }
    
    public void Init(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemSlot.TooltipIsOpened = true;
        _item = itemSlot.Item;
        
        itemName.SetText(_item.ItemName);
        itemDescription.SetText(_item.Description);
        image.sprite = _item.Sprite;
        if (!(_item is UsableItem)) optionalButton.gameObject.SetActive(false);
        //Set Effect on the weapon
        EquipmentItem equipmentItem = itemSlot.Item as EquipmentItem;
        if (equipmentItem)
        {
            if(equipmentItem._effect)
            {
                effectImage.gameObject.SetActive(true);
                effectImage.sprite = equipmentItem._effect.Icon;
            }
            //Check do we need to show the req;
            ShowReq(equipmentItem);
            //Show the Equip Button:
            optionalButton.gameObject.SetActive(true);
            var name = optionalButton.GetComponentInChildren<TextMeshProUGUI>();
            name.SetText("Equip");
        }

    }

    public void Use()
    {
        if(_itemSlot.Item as EquipmentItem)
        {
            //Must be equip logic HERE!
            //
            CloseSelf();
        }
        else
        {
            _itemSlot.Item.Use();
            _itemSlot.Amount--;
            if (_itemSlot.Amount < 1)
                CloseSelf();
        }
    }
    
    public void Drop()
    {
        DropItem?.Invoke(_item);
        CloseSelf();
    }
    
    public void CloseSelf()
    {
        CloseTooltip?.Invoke(this);
        _itemSlot.TooltipIsOpened = false;
        Destroy(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3) eventData.delta;
    }

    private void ShowReq(EquipmentItem item)
    {
        if(item.GetRequiredAgility() > 0)
        {
            _req.SetActive(true);
            _agility.SetText(item.GetRequiredAgility().ToString());
        }
        if(item.GetRequiredStrength() > 0)
        {
            _req.SetActive(true);
            _strength.SetText(item.GetRequiredStrength().ToString());
        }
        if(item.GetRequiredIntelligence() > 0)
        {
            _req.SetActive(true);
            _int.SetText(item.GetRequiredIntelligence().ToString());
        }
    }

}