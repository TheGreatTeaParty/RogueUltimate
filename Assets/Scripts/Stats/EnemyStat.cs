using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat,IDamaged
{
    public delegate void OnReceivedDamage(int damage);
    public OnReceivedDamage onReceivedDamage;

    public delegate void OnDamaged();
    public OnDamaged onDamaged;

    public delegate void OnDie();
    public OnDie onDie;

    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
     
        onReceivedDamage?.Invoke(magicDamageReceived+physicalDamageReceived);

        onDamaged?.Invoke();
    }

    public override void Die()
    {
        onDie?.Invoke();
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<FloatingNumber>().enabled = false;
        Destroy(this);
    }
}
