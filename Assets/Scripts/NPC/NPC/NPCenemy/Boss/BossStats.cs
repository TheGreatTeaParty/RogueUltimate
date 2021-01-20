public class BossStats : EnemyStat
{
    public override bool TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);
        BossFightPortal.Instance.SetBossHealth(CurrentHealth);
        return true;
    }
    
    public override void Die()
    {
        BossFightPortal.Instance.TurnThePortal();
        BossFightPortal.Instance.HealthBar(false);
        base.Die();
    }
    
}
