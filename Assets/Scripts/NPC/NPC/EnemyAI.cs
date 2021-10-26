using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyAI : AI
{
    protected EnemyStat stats;
    [SerializeField] protected float attackRadius = 0.5f;
    [SerializeField] protected float attackRange = 2f;

    protected Vector3 followPosition;
    protected bool _isTriggered = false;
    protected SurroundPositions playerPositions;

    protected NPCstate State
    {
        set
        {
            if( value == NPCstate.Chasing)
            {
                //Find the available postion arounf the player
                GameObject Target = playerPositions.GetClosestPosition(transform.position, this);
                if (Target)
                {
                    target = Target;
                }
                state = value;
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        playerPositions = PlayerOnScene.Instance.surroundPositions;

        stats = GetComponent<EnemyStat>();
        stats.onDie += Die;
        state = NPCstate.Waiting;
    }
    
    protected virtual void FixedUpdate()
    {
       /* if (Vector2.Distance(transform.position, target.transform.position) > attackRange && !isAttack
            && Vector2.Distance(transform.position, target.transform.position) <= detectionRange)
            state = NPCstate.Chasing;*/

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
            case NPCstate.Waiting:
                {
                    StateWaiting();
                    break;
                }
            case NPCstate.IDLE:
                {
                    break;
                }
        }
    }
    
    protected override void UpdatePath()
    {
        if (target)
        {
            if (seeker.IsDone())
            {
                if (state == NPCstate.Hanging)
                    seeker.StartPath(transform.position, followPosition, OnPathComplete);
                else
                {
                    seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
                }
            }
        }
    }

    protected virtual void StateHanging()
    {
        if (path != null)
        {
            if (Vector2.Distance(_collider.bounds.center,followPosition) < 1f || reachedEnd)
            {
                StopMoving();
                state = NPCstate.Waiting;
            }

            if (!isStopped)
                rb.MovePosition(transform.position + (Vector3)direction * movementSpeed * Time.deltaTime);

        }
    }
    protected virtual void StateWaiting()
    {
        StopMoving();
        if (!_isTriggered)
            EnemyTrigger();
        else
        {
            StartMoving();
            State = NPCstate.Chasing;
        }
    }
    protected void EnemyTrigger()
    {
        if (target)
        {
            if (Vector2.Distance(transform.position, target.transform.position) < detectionRange)
            {
                State = NPCstate.Chasing;
                _isTriggered = true;
                StartMoving();
            }
        }
    }

    protected override void StateChasing()
    {
        if (target)
        {
            base.StateChasing();

            if (Vector2.Distance(transform.position, target.transform.position) <= (attackRange *0.9))
                state = NPCstate.Attacking;
        }
    }

    protected void StateAttack()
    {
        if (!isAttack)
            StartCoroutine(AttackWait());
    }
    
    protected virtual void Attack()
    {

    }
    
    protected virtual IEnumerator AttackWait()
    {
        isAttack = true;
        StopMoving();
        yield return new WaitForSeconds(attackCoolDown);
        OnAttacked?.Invoke();
        yield return new WaitForSeconds(attackDuration);
        State = NPCstate.Chasing;
        Attack();
        StartMoving();
    }
    public void TriggerEnemy()
    {
        if (target)
        {
            State = NPCstate.Chasing;
            _isTriggered = true;
            StartMoving();
        }
    }
    protected void GenerateFollowPosition(Vector2 position)
    {
        if (AstarPath.active.data.gridGraph.GetNearest(position).node.Walkable)
        {
            followPosition = position;
        }
    }

    protected bool IsPositionAvailable(Vector2 position)
    {
        if (AstarPath.active.data.gridGraph.GetNearest(position).node.Walkable)
        {
            return true;
        }
        return false;
    }
    public void DisableControll()
    {
        state = NPCstate.IDLE;
    }
    public void EnableControll()
    {
        State = NPCstate.Chasing;
    }

    public void StopEnemyAttack()
    {
        StopAllCoroutines();
        isAttack = false;
        State = NPCstate.Chasing;
        StartMoving();
    }

}