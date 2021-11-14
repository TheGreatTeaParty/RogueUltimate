using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatStat : EnemyStat
{
    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        maxHealth = 8 + 4 * level + HealthBonus * level;
        PhysicalDamage.SETBASE(2 + level * 2 + DamageBonus * level);
    }
}
