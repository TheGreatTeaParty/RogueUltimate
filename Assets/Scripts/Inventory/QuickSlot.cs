using System;
using UnityEngine;
using UnityEngine.UI;


public class QuickSlot : MonoBehaviour
{
    [SerializeField] private UsableItem item;
    [SerializeField] private Button button;


    private void Start()
    {
        button.image.enabled = false;
    }


    public void AddItemToQuickAccessSlot(UsableItem usableItem)
    {
        item = usableItem;
        button.image.sprite = item.Sprite;
        button.enabled = true;
    }


    public void RemoveItemFromQuickAccessSlot()
    {
        button.image.sprite = null;
        button.enabled = false;
    }
    
    
    public void Click()
    {
        if (item != null)
            QuickSlotsManager.Instance.UseItem(item);
    }
    
    
}