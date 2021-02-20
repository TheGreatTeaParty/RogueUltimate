using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Warrior
{
    public float RollForce = 4f;
    public float RunBack = 2f;

    private bool _started = false;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRadius, _whatIsEnemy);
        bool hitted = false;
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i] != gameObject)
                hitted = enemiesToDamage[i].GetComponent<IDamaged>().
                        TakeDamage(stats.PhysicalDamage.Value, stats.MagicDamage.Value);
        }

        isAttack = false;

        state = NPCstate.Hanging;
        Vector2 direction = (target.transform.position - transform.position);
        Vector2 runBack = transform.position + (Vector3)(-direction.normalized * RunBack);
        if (IsPositionAvailable(runBack))
            Roll(-direction);

    }

  

    protected IEnumerator EnemyWait()
    {
        StopMoving();
        yield return new WaitForSeconds(waitTime);
        _started = true;
        state = NPCstate.Chasing;
        direction = nextPointDir;
        StartMoving();
        _started = false;
    }

    private void Roll(Vector2 dir)
    {
        animator.SetTrigger("Roll");
        direction = dir;
        rb.AddForce(dir.normalized * 1000 * RollForce);
        if (!_started)
            StartCoroutine(EnemyWait());
    }

}
