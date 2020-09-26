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
            bool wasPickedUp = InventoryManager.Instance.AddItemToInventory(itemOnScene.GetItem());
            if (wasPickedUp)
            {
                AudioManager.Instance.Play("Collect");
                Destroy(gameObject);
            }
        }
    }
    
    
}
