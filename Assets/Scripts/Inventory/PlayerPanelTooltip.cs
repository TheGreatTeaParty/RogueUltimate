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
    
    private Item _item;
    private int _index;
    private Image _image;
    public Button dropButton;
    public Button optionalButton;
    public TextMeshProUGUI optionalButtonText;


    private void Start()
    {
        gameObject.SetActive(false);    
    }

    
    public void ShowTooltip(Item item)
    {
        _item = item;
        _image.sprite = _item.GetSprite();
        optionalButtonText.text = "Use";
        gameObject.SetActive(true);
    }

    public void ShowTooltip(EquipmentItem equipmentItem)
    {
        _item = equipmentItem;
        optionalButtonText.text = "Equip";
        gameObject.SetActive(true);
    }

    public void ShowTooltip(EquipmentItem equipmentItem, int slotIndex)
    {
        _item = equipmentItem;
        _index = slotIndex;
        optionalButtonText.text = "Unequip";
        gameObject.SetActive(true);
    }


    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
    
    public void DropButtonPress()
    {
        _item.Drop();
        HideTooltip();
    }


    public void OptionalButtonPress()
    {
        if (optionalButtonText.text == "Equip" || optionalButtonText.text == "Use") 
            _item.Use();
        else if (optionalButtonText.text == "Unequip") 
            EquipmentManager.Instance.UnEquip(_index);

        HideTooltip();
    }
    

}
