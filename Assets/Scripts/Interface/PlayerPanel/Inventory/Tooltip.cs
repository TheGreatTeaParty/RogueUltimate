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
        //Set Effect on the weapon
        EquipmentItem equipmentItem = itemSlot.Item as EquipmentItem;
        if (equipmentItem)
        {
            if(equipmentItem._effect)
            {
                effectImage.gameObject.SetActive(true);
                effectImage.sprite = equipmentItem._effect.Icon;
            }
        }

        if (!(_item is UsableItem)) optionalButton.gameObject.SetActive(false);
    }

    public void Use()
    {
        _itemSlot.Item.Use();
        _itemSlot.Amount--;
        if (_itemSlot.Amount < 1)
            CloseSelf();
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

}