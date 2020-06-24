using UnityEngine;


public class UsableItem : Item
{
    public override void Use()
    {
        base.Use();
        
    }


    public override void MoveToQuickAccess()
    {
        base.MoveToQuickAccess();
        InventoryManager.Instance.AddItemToQuickAccessSlot(this);
    }
}