using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private ItemScene itemOnScene;

    
    private void Awake()
    {
        itemOnScene = GetComponent<ItemScene>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool wasPickedUp = Inventory.instance.AddItemToInventory(itemOnScene.GetItem());
            if (wasPickedUp)
                Destroy(gameObject);
        }
    }
    
    
}
