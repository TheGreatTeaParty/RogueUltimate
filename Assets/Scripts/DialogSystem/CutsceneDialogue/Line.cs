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
    
    [SerializeField] private AudioClip scrollingSound;
    
    
    // [SerializeField]
    // [Tooltip("Pattern of a text appearance.\nn element is responsible for the number of the character from which the speed should change.\nn + 1 element is responsible for speed.")] 
    // private int[] scrollingPattern;

    public Sprite Icon => icon;
    public string CharacterName => characterName;
    public string DialogueLine => dialogueLine;
    public Position IconPosition => iconPosition;
    public AudioClip ScrollingSound => scrollingSound;
}
