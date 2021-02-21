using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreStat : EnemyStat
{
    public override void SetLevel(int level)
    {
        maxHealth = 20 + 7 * level;
        PhysicalDamage.SETBASE(3 + level);
    }
}
