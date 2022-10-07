using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Warrior
{
    public float RollForce = 400f;
    public float RunBack = 2f;

    private bool _started = false;
    private Animator animator;

    
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void StateWaiting()
    {
        StopMoving();
        if (!_isTriggered)
            EnemyTrigger();
        else
        {
            return;
        }
    }
    protected override void StateIDLE()
    {
        base.StateIDLE();
        animator.SetFloat("Speed", 0);
    }

    protected override void Attack()
    {
        GoblinStat _goblinStats = stats as GoblinStat;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRadius, _whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i] != gameObject)
                enemiesToDamage[i].GetComponent<IDamaged>().
                        TakeDamage(stats.PhysicalDamage.Value, stats.MagicDamage.Value);

            if (Random.value < _goblinStats.PoisonChance)
            {
                CharacterStat character = enemiesToDamage[i].GetComponent<CharacterStat>();
                if (character)
                    character.EffectController.AddEffect(Instantiate(_goblinStats.PoisonEffect), character);
            }
        }

        Vector2 direction = Vector2.zero;
        if (target)
            direction = (target.transform.position - transform.position);
        Vector2 runBack = transform.position + (Vector3)(-direction.normalized * RunBack);
        DisableControll();
        Roll(-direction);
    }



    protected IEnumerator EnemyWait()
    {
        yield return new WaitForSeconds(waitTime);
        EnableControll();
        direction = nextPointDir;
        isAttack = false;
    }

    private void Roll(Vector2 dir)
    {
        animator.SetTrigger("Roll");
        direction = dir;
        rb.AddForce(dir.normalized * 750 * RollForce);
        StartCoroutine(EnemyWait());
    }

}
