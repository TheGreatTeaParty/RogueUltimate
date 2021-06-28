using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract : Item
{
    public enum contractType
    {
        Regular = 0,
        Major = 1,
    };
    public contractType type;
    public int _currentScore;

    public int AimScore = 10;

    public virtual void StartContract()
    {


    }
}
