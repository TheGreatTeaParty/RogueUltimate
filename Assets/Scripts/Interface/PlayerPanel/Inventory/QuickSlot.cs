using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuickSlot : ItemSlot
{
    [SerializeField] private UsableItem item;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private void Awake()
    {
        button.image.enabled = false;
        button.enabled = false;
    }


    public void AddItemToQuickAccessSlot(UsableItem usableItem)
    {
        item = usableItem;
        button.image.sprite = item.Sprite;
        button.image.enabled = true;
        button.enabled = true;
    }


    public void RemoveItemFromQuickAccessSlot()
    {
        item = null;
        button.image.sprite = null;
        button.image.enabled = false;
        button.enabled = false;
    }
    
    
    public void Click()
    {
        if (item != null)
            QuickSlotsManager.Instance.UseItem(item);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }
}