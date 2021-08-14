using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/FireBall")]
public class FireBall : ActiveAbility
{
    [SerializeField]
    private Transform fireball;
    private PlayerMovement movement;
    private PlayerStat playerStat;

    public override void Activate()
    {
        base.Activate();
        playerStat = CharacterManager.Instance.Stats;
        movement = playerStat.playerMovement;
        Cast();
    }

    private void Cast()
    {
        if (movement.GetLockMoving())
        {
            Vector3 direction = movement.GetTargetLock().GetDir();
            Transform magic = Instantiate(fireball,
                movement.gameObject.transform.position + direction.normalized, Quaternion.identity);
            var crit = playerStat.GetMagicalCritDamage();
            magic.GetComponent<FlyingObject>().SetData(0f, playerStat.Intelligence.GetBaseValue(), direction, crit.Item2);
        }
        else
        {
            Vector3 direction = movement.GetDirection();
            Transform magic = Instantiate(fireball,
                movement.gameObject.transform.position + direction.normalized, Quaternion.identity);
            var crit = playerStat.GetMagicalCritDamage();
            magic.GetComponent<FlyingObject>().SetData(0f, playerStat.Intelligence.GetBaseValue(), direction, crit.Item2);
        }
    }
}
