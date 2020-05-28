using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAsset : MonoBehaviour
{
    public static ItemsAsset instance;

    private void Awake()
    {
        instance = this;
    }

    public Item[] items;
    public Transform Item_prefab;

}
