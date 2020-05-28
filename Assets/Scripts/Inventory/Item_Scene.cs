using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Scene : MonoBehaviour
{
    public static Item_Scene SpawnItemScene(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemsAsset.instance.Item_prefab, position, Quaternion.identity);

        Item_Scene item_on_scene = transform.GetComponent<Item_Scene>();
        item_on_scene.SetItem(item);

        return item_on_scene;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
