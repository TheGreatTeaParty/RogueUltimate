using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcStat : EnemyStat
{
    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        maxHealth = 5 + 5 * level + HealthBonus * level;
        PhysicalDamage.SETBASE(1 + level * 2 + DamageBonus * level);
        if(level >= 4)
            PhysicalProtection.SETBASE(level-3);
        else
            PhysicalProtection.SETBASE(0);
    }
}
