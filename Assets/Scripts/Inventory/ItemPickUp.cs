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
        if(collision.tag == "Player")
        {
            bool WasPickedUp = Inventory.instance.Add(itemOnScene.GetItem());
            if (WasPickedUp)
                Destroy(gameObject);
        }
    }
    
    
}
