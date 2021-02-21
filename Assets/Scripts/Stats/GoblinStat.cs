using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : EnemyStat
{
    public override void SetLevel(int level)
    {
        maxHealth = 6 + 3 * level;
        PhysicalDamage.SETBASE(2 + level * 5);
    }
}
