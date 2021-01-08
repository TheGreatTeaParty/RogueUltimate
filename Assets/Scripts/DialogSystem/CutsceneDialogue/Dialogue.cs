using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    private enum Language
    {
        English = 0
    }
    
    [SerializeField] private Line[] lines;
    [SerializeField] private Language language = Language.English;
    
    public Line[] Lines => lines;
}
