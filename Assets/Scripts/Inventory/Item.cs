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
    
    [Space] 
    
    public int stackSize;
    public int price;
}

