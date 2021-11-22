using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MeleeWeapon/MeleRangeWeapon")]
public class MeleRangeWeapon : MeleeWeapon
{
    [Space]
    [SerializeField]
    float damageReduction = 0.35f;
    [SerializeField]
    float meleAttackRange = 1.4f;
    [SerializeField] Transform rangeProjectile;

    public override void Attack(float physicalDamage, float magicDamage)
    {
        PlayerStat playerStat = CharacterManager.Instance.Stats;
        if (!playerStat.ModifyMana(requiredMana) ||
          !playerStat.ModifyHealth(requiredHealth) ||
          !playerStat.ModifyStamina(requiredStamina))
            return;
        var player = PlayerOnScene.Instance;

        _whatIsEnemy = LayerMask.GetMask("Enemy", "EnvObjects");

        if (player.playerMovement.GetTargetLock().CheckTheDistance() <= meleAttackRange)
        {
            Vector3 direction;
            if (player.playerMovement.GetLockMoving() == true) { direction = player.playerMovement.GetTargetLock().GetDir(); }
            else { direction = player.playerMovement.GetDirection(); }
            _attackPosition = player.playerMovement.PlayerCollider.bounds.center + direction.normalized / 1.5f;

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, CharacterManager.Instance.Stats.AttackRange.Value, _whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                //If take damage returns true -> play hit effect:
                if (enemiesToDamage[i].gameObject != player.gameObject)
                {
                    var crit = playerStat.GetPhysicalCritDamage();
                    enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(crit.Item1, magicDamage, crit.Item2);

                    //Assign Effect:
                    if (_effect)
                    {
                        if (Random.value < _effect._chance)
                        {
                            CharacterStat character = enemiesToDamage[i].GetComponent<CharacterStat>();
                            if (character)
                                character.EffectController.AddEffect(Instantiate(_effect), character);
                        }
                    }


                    //Create Visual Effect:
                    if (HitEffect)
                    {
                        Transform Effect = Instantiate(HitEffect, enemiesToDamage[i].GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                        if (_weaponRenderer.PrevIndex == 1)
                            Effect.rotation = Quaternion.Euler(0, 0, 90f);
                        Effect.GetComponent<SpriteRenderer>().sortingOrder = enemiesToDamage[i].GetComponent<SpriteRenderer>().sortingOrder + 1;
                    }
                    else
                        Debug.LogWarning("No Hit effect assigned to the weapon!");

                    Rigidbody2D rigidbody = enemiesToDamage[i].GetComponent<Rigidbody2D>();
                    if (rigidbody)
                        rigidbody.AddForce(direction * 10000 * playerStat.KnockBack.Value);
                }
            }
            if (enemiesToDamage.Length > 0)
                ScreenShakeController.Instance.StartShake(.09f, .05f + playerStat.PushForce.Value / 1000);
        }
        //RANGE attack:
        else
        {
            Vector3 direction = player.playerMovement.GetTargetLock().GetDir();
            if (!playerStat.ModifyMana(requiredMana) ||
              !playerStat.ModifyHealth(requiredHealth) ||
              !playerStat.ModifyStamina(requiredStamina))
                    return;
                Transform arrow = Instantiate(rangeProjectile,
                    PlayerOnScene.Instance.playerMovement.transform.position + direction, Quaternion.identity);
                var crit = playerStat.GetPhysicalCritDamage();

                if (_effect)
                    arrow.GetComponent<FlyingObject>().SetData(crit.Item1 * (1 - damageReduction), magicDamage * (1 - damageReduction), direction, crit.Item2, playerStat.KnockBack.Value, Instantiate(_effect));
                else 
                    arrow.GetComponent<FlyingObject>().SetData(crit.Item1 * (1 - damageReduction), magicDamage * (1 - damageReduction), direction, crit.Item2, playerStat.KnockBack.Value);
        }
    }
}
