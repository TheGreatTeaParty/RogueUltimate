using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AI : MonoBehaviour
{
    public float nextWayPointDistance = 1f;
    public float Speed = 4f;
    public float AttackRange = 0.5f;
    public float DetectionRange = 7f;
    public float FollowRange = 10f;
    public float waitTime = 2f;
    [Space]
    public float attackCoolDown = 1.2f;

    protected float startAttackCoolDown;
    protected Vector3[] points;
    protected NPCstate state;
    protected int currentPathpoint = 0;
    protected bool reachedEnd = false;
    protected Vector2 dir;
    protected Vector2 last_dir;
    protected GameObject target;

    protected Path path;
    protected Seeker seeker;
    protected Rigidbody2D Rb;
    private bool _stopped = false;


    // Start is called before the first frame update
    public virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        Rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        startAttackCoolDown = attackCoolDown;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (startAttackCoolDown > 0)
            startAttackCoolDown -= Time.deltaTime;

        if (path == null) //Change it!
            return;

        if (currentPathpoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }

        else
        {
            reachedEnd = false;
        }

        dir = (path.vectorPath[currentPathpoint] - transform.position).normalized;

        if (dir != Vector2.zero)
        {
            last_dir = dir;
        }

        if(Vector2.Distance(Rb.position, path.vectorPath[currentPathpoint]) < nextWayPointDistance)
        {
            currentPathpoint++;
        }
    }

    public virtual void FixedUpdate()
    {
        switch (state)
        {
            case NPCstate.chasing:
                {
                    StateChasing();
                    break;
                }

            case NPCstate.hanging:
                {
                    StateHanging();
                    break;
                }

            case NPCstate.attacking:
                {
                    StateAttack();
                    break;
                }
        }
    }

    void OnPathComplete(Path _path)
    {
        if (!_path.error)
        {
            path = _path;
            currentPathpoint = 0;
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
    }


    protected void StateAttack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > AttackRange)
            state = NPCstate.chasing;

        if (startAttackCoolDown <= 0)
        {
            Attack();
            startAttackCoolDown = attackCoolDown;
        }
    }

    protected void StateHanging()
    {
        if (path != null)
        {
            //Hangout

            EnemyTrigger();
        }
    }

    protected void EnemyTrigger()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < DetectionRange)
            state = NPCstate.chasing;
    }

    public virtual void Attack()
    {

    }

    public void StateChasing()
    {
         if (path != null)
         {
             if (Vector2.Distance(transform.position, target.transform.position) <= AttackRange)
             {
                 state = NPCstate.attacking;
             }
            else
            {
                if(!_stopped)
                    Rb.MovePosition(Rb.position + dir * Speed * Time.deltaTime);
            }
             Relax();
         }
    }

    private void Relax()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > FollowRange)
        {
            state = NPCstate.hanging;
        }
    }

    public enum NPCstate
    {
        hanging = 0,
        chasing,
        attacking,
    }

    public void StopMoving()
    {
        _stopped = true;
    }

    public void StartMoving()
    {
        _stopped = false;
    }
}