public class BossStats : EnemyStat
{
    public override void TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);
        BossFightPortal.Instance.SetBossHealth(CurrentHealth);
    }
    
    public override void Die()
    {
        BossFightPortal.Instance.TurnThePortal();
        BossFightPortal.Instance.HealthBar(false);
        base.Die();
    }
    
}
