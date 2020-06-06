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
    [SerializeField] TextMeshProUGUI optionalButtonText;
    [Space]
    [SerializeField] private Button dropButton;
    [SerializeField] private Button optionalButton;


    private void Start()
    {
        gameObject.SetActive(false);    
    }

    
    public void ShowTooltip(Item reusableItem)
    {
        item = reusableItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        optionalButtonText.text = "Use";
        gameObject.SetActive(true);
    }

    public void ShowTooltip(EquipmentItem equipmentItem)
    {
        item = equipmentItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        optionalButtonText.text = "Equip";
        gameObject.SetActive(true);
    }

    public void ShowTooltip(EquipmentItem equipmentItem, int slotIndex)
    {
        item = equipmentItem;
        itemName.SetText(item.Name);
        image.sprite = item.Sprite;
        _index = slotIndex;
        optionalButtonText.text = "Unequip";
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
    

}
