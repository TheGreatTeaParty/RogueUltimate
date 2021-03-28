using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatStat : EnemyStat
{
    public override void SetLevel(int level)
    {
        maxHealth = 8 + 4 * level;
        PhysicalDamage.SETBASE(2 + level * 3);
    }
}
