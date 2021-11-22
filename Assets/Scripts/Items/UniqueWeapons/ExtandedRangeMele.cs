using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MeleeWeapon/LongRangeWeapon")]
public class ExtandedRangeMele : MeleeWeapon
{
    public override void Attack(float physicalDamage, float magicDamage)
    {
        PlayerStat playerStat = CharacterManager.Instance.Stats;
        if (!playerStat.ModifyMana(requiredMana) ||
          !playerStat.ModifyHealth(requiredHealth) ||
          !playerStat.ModifyStamina(requiredStamina))
            return;
        var player = PlayerOnScene.Instance;

        _whatIsEnemy = LayerMask.GetMask("Enemy", "EnvObjects");
        Vector3 direction;
        if (player.playerMovement.GetLockMoving() == true) { direction = player.playerMovement.GetTargetLock().GetDir(); }
        else { direction = player.playerMovement.GetDirection(); }
        _attackPosition = player.playerMovement.PlayerCollider.bounds.center + direction.normalized / 1.5f;

        RaycastHit2D enemyToDamage = Physics2D.Raycast(player.playerMovement.PlayerCollider.bounds.center,direction,playerStat.AttackRange.Value, _whatIsEnemy);

        if(enemyToDamage)
        {
            //If take damage returns true -> play hit effect:
            if (enemyToDamage.collider.gameObject != player.gameObject)
            {
                var crit = playerStat.GetPhysicalCritDamage();
                enemyToDamage.collider.GetComponent<IDamaged>().TakeDamage(crit.Item1, magicDamage, crit.Item2);

                //Assign Effect:
                if (_effect)
                {
                    if (Random.value < _effect._chance)
                    {
                        CharacterStat character = enemyToDamage.collider.GetComponent<CharacterStat>();
                        if (character)
                            character.EffectController.AddEffect(Instantiate(_effect), character);
                    }
                }


                //Create Visual Effect:
                if (HitEffect)
                {
                    Transform Effect = Instantiate(HitEffect, enemyToDamage.collider.GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                    if (_weaponRenderer.PrevIndex == 1)
                        Effect.rotation = Quaternion.Euler(0, 0, 90f);
                    Effect.GetComponent<SpriteRenderer>().sortingOrder = enemyToDamage.collider.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                else
                    Debug.LogWarning("No Hit effect assigned to the weapon!");

                Rigidbody2D rigidbody = enemyToDamage.collider.GetComponent<Rigidbody2D>();
                if (rigidbody)
                    rigidbody.AddForce(direction * 10000 * playerStat.KnockBack.Value);
            }
        }
        if (enemyToDamage)
            ScreenShakeController.Instance.StartShake(.09f, .05f + playerStat.PushForce.Value / 1000);
    }
}
