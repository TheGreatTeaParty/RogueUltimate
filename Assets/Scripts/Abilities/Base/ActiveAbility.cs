using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/ActiveAbility")] 
public class ActiveAbility : Ability
{
    protected float _timeUntilCast = 0;
    public float coolDownTime;


    protected virtual void Update()
    {
        Debug.Log("update");
        if (_timeUntilCast > 0)
        {
            _timeUntilCast -= Time.deltaTime;
            return;
        }
    }

    public virtual void Activate()
    {
        if (_timeUntilCast > 0)
            return;
        
        _timeUntilCast = coolDownTime;
    }
    
}