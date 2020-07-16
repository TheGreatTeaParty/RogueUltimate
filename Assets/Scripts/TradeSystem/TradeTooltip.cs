using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeTooltip : MonoBehaviour
{
    #region Singleton
    
    public static TradeTooltip Instance;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    #endregion
    
    [SerializeField] private Image itemImage;
    [SerializeField] private Image coinImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;


    public void Show(Item item)
    {
        if (item == null) return;
        ;
        ChangeSprite(item.Sprite, true);
        ChangeName(item.Name, true);
        ChangePrice(item.Price, true);
    }
    
    public void Erase()
    {
        ChangeSprite(null, false);
        ChangeName("", false);
        ChangePrice(0, false);
    }

    private void ChangeSprite(Sprite sprite, bool state)
    {
        itemImage.sprite = sprite;
        itemImage.enabled = state;
    }

    private void ChangeName(string text, bool state)
    {
        itemName.SetText(text);
        itemName.enabled = state;
    }

    private void ChangePrice(int value, bool state)
    {
        itemPrice.SetText(value.ToString());
        itemName.enabled = state;
    }
    
} 