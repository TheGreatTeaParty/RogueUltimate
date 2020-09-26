using UnityEngine;


public class UsableItem : Item
{
    public override void Use()
    {
        base.Use();
        
        ModifyStats();
        InventoryManager.Instance.RemoveItemFromInventory(this);
        QuickSlotsManager.Instance.Request(this);
    }


    // Use this function 
    public virtual void ModifyStats()
    {
        
    }
    

    public override void MoveToQuickAccess()
    {
        base.MoveToQuickAccess();
        AudioManager.Instance.Play("Click");
        QuickSlotsManager.Instance.AddItemToQuickAccessSlot(this);
    }
    
    
}