using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MeleeWeapon/SplashWeapon")]
public class SplashSword : MeleeWeapon
{
    [Space]
    [SerializeField] private float splashRadius = 1f;
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

        Collider2D enemyToDamage = Physics2D.OverlapCircle(_attackPosition, CharacterManager.Instance.Stats.AttackRange.Value, _whatIsEnemy);
      
            //If take damage returns true -> play hit effect:
            if (enemyToDamage.gameObject != player.gameObject)
            {
                var crit = playerStat.GetPhysicalCritDamage();
                enemyToDamage.GetComponent<IDamaged>().TakeDamage(crit.Item1, magicDamage, crit.Item2);

                //Assign Effect:
                if (_effect)
                {
                    if (Random.value < _effect._chance)
                    {
                        CharacterStat character = enemyToDamage.GetComponent<CharacterStat>();
                        if (character)
                            character.EffectController.AddEffect(Instantiate(_effect), character);
                    }
                }


                //Create Visual Effect:
                if (HitEffect)
                {
                    Transform Effect = Instantiate(HitEffect, enemyToDamage.GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                    if (_weaponRenderer.PrevIndex == 1)
                        Effect.rotation = Quaternion.Euler(0, 0, 90f);
                    Effect.GetComponent<SpriteRenderer>().sortingOrder = enemyToDamage.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                else
                    Debug.LogWarning("No Hit effect assigned to the weapon!");

                Rigidbody2D rigidbody = enemyToDamage.GetComponent<Rigidbody2D>();
                if (rigidbody)
                    rigidbody.AddForce(direction * 10000 * playerStat.KnockBack.Value);

                DealSplashDamage(enemyToDamage.transform.position, (int)(crit.Item1 / 3), enemyToDamage);
            }
        if (enemyToDamage)
            ScreenShakeController.Instance.StartShake(.09f, .05f + playerStat.PushForce.Value / 1000);
    }
    private void DealSplashDamage(Vector3 position, float damage, Collider2D self)
    {
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(position, splashRadius, _whatIsEnemy);
        foreach (var enemy in enemyToDamage)
        {
            if (enemy && enemy != self)
            {
                enemy.GetComponent<IDamaged>().TakeDamage(damage, 0, false);

                //Assign Effect:
                if (_effect)
                {
                    if (Random.value < _effect._chance)
                    {
                        CharacterStat character = enemy.GetComponent<CharacterStat>();
                        if (character)
                            character.EffectController.AddEffect(Instantiate(_effect), character);
                    }
                }
                //Create Visual Effect:
                if (HitEffect)
                {
                    Transform Effect = Instantiate(HitEffect, enemy.GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                    if (_weaponRenderer.PrevIndex == 1)
                        Effect.rotation = Quaternion.Euler(0, 0, 90f);
                    Effect.GetComponent<SpriteRenderer>().sortingOrder = enemy.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
            }
        }
    }
}
