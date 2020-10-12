﻿using UnityEngine;

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
            bool wasPickedUp = CharacterManager.Instance.Inventory.AddItem(itemOnScene.GetItem().GetCopy());
            if (wasPickedUp)
            {
                AudioManager.Instance.Play("Collect");
                Destroy(gameObject);
            }
        }
    }
    
    
}