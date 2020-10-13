using System;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/ActiveAbility")] 
public class ActiveAbility : Ability
{
    protected float _timeUntilCast;
    public float coolDownTime;


    protected void Update()
    {
        if (_timeUntilCast > 0)
            _timeUntilCast -= Time.deltaTime;
    }

    public virtual void Activate()
    {
        if (_timeUntilCast > 0)
            return;
    }
    
}