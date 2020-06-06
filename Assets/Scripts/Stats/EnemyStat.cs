using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    public delegate void OnRecievedDamage(int damage, bool type);
    public OnRecievedDamage onRecievedDamage;

    public override void TakeDamage(int ph_damage, int mg_damage)
    {
        base.TakeDamage(ph_damage, mg_damage);

        if(ph_damage_recieved!= 0)
            onRecievedDamage?.Invoke(ph_damage_recieved,true);
        if(mg_damage_recieved!= 0)
            onRecievedDamage?.Invoke(mg_damage_recieved,false);
    }

}
