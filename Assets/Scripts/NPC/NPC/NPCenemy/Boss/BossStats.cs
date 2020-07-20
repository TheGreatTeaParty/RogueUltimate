using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : EnemyStat
{
    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
        BossFightPortal.Instance.SetBossHealth(currentHealth);
    }
    public override void Die()
    {
        BossFightPortal.Instance.TurnThePortal();
        BossFightPortal.Instance.HealthBar(false);
        base.Die();
    }
}
