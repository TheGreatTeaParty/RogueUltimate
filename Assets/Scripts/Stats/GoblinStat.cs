using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : EnemyStat
{
    public float PoisonChance;

    public Effect PoisonEffect;

    public override void SetLevel(int level)
    {
        maxHealth = 6 + 3 * level;
        PhysicalDamage.SETBASE(2 + level * 5);
        PoisonChance = 0.2f + 0.1f * level;
    }
}
