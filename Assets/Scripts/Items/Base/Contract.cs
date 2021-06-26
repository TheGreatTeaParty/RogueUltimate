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

    public virtual void StartContract()
    {


    }
}
