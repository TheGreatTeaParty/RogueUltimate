using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PickUp : MonoBehaviour
{
    Item_Scene item_on_scene;

    private void Awake()
    {
        item_on_scene = GetComponent<Item_Scene>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            bool WasPickedUp = Inventory.instance.Add(item_on_scene.GetItem());
            if (WasPickedUp)
                Destroy(gameObject);
        }
    }
}
