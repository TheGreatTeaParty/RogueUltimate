using UnityEngine;


public class ItemScene : MonoBehaviour
{
    public static ItemScene SpawnItemScene(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemsAsset.instance.Item_prefab, position, Quaternion.identity);

        ItemScene itemOnScene = transform.GetComponent<ItemScene>();
        itemOnScene.SetItem(item);

        return itemOnScene;
    }

    private Item _item;
    private SpriteRenderer _spriteRenderer;

    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    public void SetItem(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = item.Sprite;
    }

    
    public Item GetItem()
    {
        return _item;
    }

    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    
}
