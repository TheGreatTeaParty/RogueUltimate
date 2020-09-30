using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : EnemyStat
{
    public override void TakeDamage(float physicalDamage, float magicDamage)
    {
        base.TakeDamage(physicalDamage, magicDamage);
        BossFightPortal.Instance.SetBossHealth(CurrentHealth);
    }
    public override void Die()
    {
        BossFightPortal.Instance.TurnThePortal();
        BossFightPortal.Instance.HealthBar(false);
        base.Die();
    }
}
