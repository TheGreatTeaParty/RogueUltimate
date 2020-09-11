using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat,IDamaged
{
    [Space] public int XP = 250;

    public delegate void OnReceivedDamage(int damage);
    public OnReceivedDamage onReceivedDamage;

    public delegate void OnDamaged();
    public OnDamaged onDamaged;

    public delegate void OnDie();
    public OnDie onDie;


    public void Start()
    {
        int modifier = level * 10; 
        
        physicalDamage.AddModifier(modifier);
        magicDamage.AddModifier(modifier);
        physicalProtection.AddModifier(modifier);
        magicProtection.AddModifier(modifier);
    }
    
    public override void TakeDamage(int _physicalDamage, int _magicDamage)
    {
        base.TakeDamage(_physicalDamage, _magicDamage);
     
        onReceivedDamage?.Invoke(magicDamageReceived+physicalDamageReceived);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        onDamaged?.Invoke();
    }

    public override void Die()
    {
        PlayerStat.Instance.GainXP(XP);
        onDie?.Invoke();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<FloatingNumber>().enabled = false;
        Destroy(this);
    }
    
}
