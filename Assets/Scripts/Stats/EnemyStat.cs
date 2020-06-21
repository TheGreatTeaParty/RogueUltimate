using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat,IDamaged
{
    public delegate void OnReceivedDamage(int damage, bool type);
    public OnReceivedDamage onReceivedDamage;

    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
        if (physicalDamageReceived != 0)
            onReceivedDamage?.Invoke(physicalDamageReceived,true);
        
        if (magicDamageReceived != 0)
            onReceivedDamage?.Invoke(magicDamageReceived,false);
    }

}
