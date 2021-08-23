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
        Contract killBoss = CharacterManager.Instance.Stats.PlayerContracts.FindContract(3);
        if (killBoss)
            killBoss._currentScore++;
        BossFightPortal.Instance.TurnThePortal();
        BossFightPortal.Instance.HealthBar(false);
        base.Die();
    }
    
}
