using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    public static TargetCircle Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
