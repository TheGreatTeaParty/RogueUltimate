using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    [SerializeField] private Button dropButton;
    [SerializeField] private Button optionalButton;
    [SerializeField] private Button quickAccessButton;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    
    public void ShowTooltip(Item pureItem)
    {
        item = pureItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        optionalButton.enabled = false;
        quickAccessButton.enabled = false;
        gameObject.SetActive(true);
    }
    
    public void ShowTooltip(EquipmentItem equipmentItem)
    {
        item = equipmentItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;

        quickAccessButton.enabled = false;
        optionalButton.enabled = true;
        optionalButtonText.text = "Equip";
        
        gameObject.SetActive(true);
    }

    public void ShowTooltip(EquipmentItem equipmentItem, int slotIndex)
    {
        item = equipmentItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        _index = slotIndex;

        quickAccessButton.enabled = false;
        optionalButton.enabled = true;
        optionalButtonText.text = "Unequip";
        
        gameObject.SetActive(true);
    }
    
    public void ShowTooltip(UsableItem usableItem)
    {
        item = usableItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;

        quickAccessButton.enabled = true;
        optionalButton.enabled = true;
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
    }
    
    
}
