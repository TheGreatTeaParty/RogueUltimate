using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Line
{
    public enum Position
    {
        Left = 0,
        Right = 1
    }
    
    [SerializeField] private Sprite icon;
    [SerializeField] private string characterName;
    [SerializeField] private string dialogueLine;
    [SerializeField] private Position iconPosition;
    
    public Sprite Icon => icon;
    public string CharacterName => characterName;
    public string DialogueLine => dialogueLine;
    public Position IconPosition => iconPosition;
}
