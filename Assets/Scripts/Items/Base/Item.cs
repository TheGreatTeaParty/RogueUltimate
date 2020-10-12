﻿using System;
using UnityEngine;
using System.Text;


public enum Tier
{
    Null = 0,
    First = 1,
    Second = 2,
    Third = 3,
    Fourth = 4,
    Fifth = 5
}

[CreateAssetMenu(menuName = "Items/Item")]  
public class Item : ScriptableObject
{
    [SerializeField] public int ID;
    [SerializeField] public Tier tier;
    [SerializeField] protected int price;
    [Space]
    [SerializeField] protected String itemName;
    [SerializeField] protected String description;
    [SerializeField] protected Sprite sprite;
    [Space] 
    [Range(1, 100)]
    [SerializeField] protected int stackMaxSize;

    public int Price => price;
    public String ItemName => itemName;
    public Sprite Sprite => sprite;
    public string Description => description;
    public int StackMaxSize
    {
        get => stackMaxSize;
        set => stackMaxSize = value;
    }


    public virtual Item GetCopy()
    {
        return this;
    }
    
    public virtual void Use()
    {
        
    }

    public virtual void MoveToQuickAccess()
    {
        
    }

}
