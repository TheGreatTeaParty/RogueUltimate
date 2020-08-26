using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TradeTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image image;

    public void SetName(String name)
    {
        this.itemName.SetText(name);
    }

    public void SetDescription(String description)
    {
        this.description.SetText(description);
    }

    public void SetPrice(int price)
    {
        if (price == -1)
        {
            this.price.enabled = false;
            return;
        }
        
        this.price.SetText(price.ToString());
    }

    public void SetSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            priceText.enabled = false;
            image.enabled = false;
            return;
        }
        
        image.sprite = sprite;
        image.enabled = true;
        priceText.enabled = true;
    }
    
} 