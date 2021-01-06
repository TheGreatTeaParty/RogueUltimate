using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField]private Line[] lines;
    
    public Line[] Lines => lines;
    public bool isActionRequired = false;
}
