using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : EnemyStat
{
    public float PoisonChance;

    public Effect PoisonEffect;

    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        maxHealth = HealthBonus + 6 + 3 * level;
        PhysicalDamage.SETBASE(DamageBonus + 2 + level * 3);
        PoisonChance = 0.2f + 0.05f * level;
    }
}
