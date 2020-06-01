using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour
{
    #region Singleton
    public static Tooltip Instance;
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    #endregion


    public Transform Transform;

}