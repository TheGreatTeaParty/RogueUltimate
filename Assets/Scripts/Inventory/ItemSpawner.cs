using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        Item_Scene.SpawnItemScene(transform.position, item);
        Destroy(gameObject);
    }
}
