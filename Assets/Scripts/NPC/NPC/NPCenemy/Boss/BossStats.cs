using Firebase.Analytics;

public class BossStats : EnemyStat
{
    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        maxHealth = 220 + (float)((3+1.8*level) * level);
        PhysicalDamage.SETBASE(level*5 - 16);
        PhysicalProtection.SETBASE(4 + level);
        MagicProtection.SETBASE(2 + level);
    }

    public override bool TakeDamage(float phyDamage, float magDamage)
    {
        base.TakeDamage(phyDamage, magDamage);
        return true;
    }
    
    public override void Die()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventUnlockAchievement, new Parameter(FirebaseAnalytics.EventUnlockAchievement, "daddy_killed"));
        Contract killBoss = CharacterManager.Instance.Stats.PlayerContracts.FindContract(3);
        if (killBoss)
            killBoss._currentScore++;
        BossFightPortal.Instance.TurnThePortal();
        BossFightPortal.Instance.HealthBar(false);
        base.Die();
    }
    
}
