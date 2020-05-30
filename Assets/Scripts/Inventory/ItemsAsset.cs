using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAsset : MonoBehaviour
{
    #region Singleton
    public static ItemsAsset instance;
    void Awake()
    {
        if (instance != null)
            return;
        
        instance = this;
    }
    #endregion

    public Item[] items;
    public Transform Item_prefab;

}
