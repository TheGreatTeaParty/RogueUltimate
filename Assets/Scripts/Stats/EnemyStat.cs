using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    public delegate void OnRecievedDamage(int damage);
    public OnRecievedDamage onRecievedDamage;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        onRecievedDamage?.Invoke(damage_recieved);
    }

}
