using System.Collections;
using UnityEngine;

public class EnemyAI : AI
{
    protected EnemyStat stats;
    [SerializeField] protected float attackRange = 0.5f;


    protected override void Start()
    {
        base.Start();
        
        stats = GetComponent<EnemyStat>();
        stats.onDie += Die;
    }
    
    protected virtual void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > attackRange && !isAttack)
            state = NPCstate.Chasing;

        switch (state)
        {
            case NPCstate.Chasing:
            {
                StateChasing();
                break;
            }

            case NPCstate.Attacking:
            {
                StateAttack();
                break;
            }
            
            case NPCstate.Hanging:
            {
                StateHanging();
                break;
            }
        }
    }

    private void StateHanging()
    {
        if (path != null)
        {
            //Hangout

            EnemyTrigger();
        }
    }
    
    private void EnemyTrigger()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < detectionRange)
            state = NPCstate.Chasing;
    }

    protected override void StateChasing()
    {
        base.StateChasing();
        
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
            state = NPCstate.Attacking;
    }

    protected void StateAttack()
    {
        if (!isAttack)
            StartCoroutine(AttackWait());
    }
    
    protected virtual void Attack()
    {

    }
    
    IEnumerator AttackWait()
    {
        isAttack = true;
        StopMoving();
        yield return new WaitForSeconds(attackCoolDown);
        OnAttacked?.Invoke();
        yield return new WaitForSeconds(attackDuration);
        Attack();
        StartMoving();
    }
    
}