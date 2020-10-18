public class UsableItem : Item
{
    public override void Use()
    {
        base.Use();
        
        ModifyStats();
    }
    
    public virtual void ModifyStats()
    {
        
    }
    
}