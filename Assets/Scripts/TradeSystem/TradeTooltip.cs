using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeTooltip : MonoBehaviour
{
    private TextMeshProUGUI name;
    private TextMeshProUGUI description;
    private TextMeshProUGUI price;
    private Image image;
    [SerializeField] private Image goldImage;


    public void SetName(String name)
    {
        this.name.SetText(name);
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
            goldImage.enabled = false;
            image.enabled = false;
            return;
        }
        
        image.sprite = sprite;
        image.enabled = true;
        goldImage.enabled = true;
    }
    
} 