using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Items/Item")]  
public class Item : ScriptableObject
{
    public String itemName;
    public Sprite itemIcon;
    public Sprite itemSprite;
    
    [Space] 
    
    public int stackSize;
    public int price;

    public Sprite GetSprite()
    {
        return itemSprite;
    }
}

