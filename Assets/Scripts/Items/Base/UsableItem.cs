using UnityEngine;


public class UsableItem : Item
{
    public override void Use()
    {
        base.Use();
        
        ModifyStats();
    }
    
    // Use this function 
    public virtual void ModifyStats()
    {
        
    }
    
}