using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpawner : MonoBehaviour
{
    public Item item;

    
    private void Start()
    {
        ItemScene.SpawnItemScene(transform.position, item);
        Destroy(gameObject);
    }
    
    
}
