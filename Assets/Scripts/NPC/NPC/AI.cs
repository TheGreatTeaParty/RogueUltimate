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
    public float attackDuration = 0.5f;

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

    private EnemyStat NPCstat;
    private bool _stopped = false;
    private bool _attack = false;

    public delegate void OnAttacked();
    public OnAttacked onAttacked;

    // Start is called before the first frame update
    public virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        Rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        NPCstat = GetComponent<EnemyStat>();
        NPCstat.onDie += Die;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    public virtual void Update()
    {
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
        if (!_attack)
            StartCoroutine(AttackWait());
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
                    Rb.MovePosition(transform.position + (Vector3)dir * Speed * Time.deltaTime);
            }
         }
    }

   
    public enum NPCstate
    {
        chasing = 0,
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

    public Vector2 GetDirection()
    {
      
        return dir;

    }
    public Vector2 GetVelocity()
    {
        if (_stopped)
        {
            return new Vector2(0f, 0f);
        }
        else
        {
            return dir;
        }
    }

    IEnumerator AttackWait()
    {
        _attack = true;
        StopMoving();
        yield return new WaitForSeconds(attackCoolDown);
        onAttacked?.Invoke();
        yield return new WaitForSeconds(attackDuration);
        _attack = true;
        Attack();
        StartMoving();
        _attack = false;
    }

    public virtual void Die()
    {
        Destroy(this);
    }
}