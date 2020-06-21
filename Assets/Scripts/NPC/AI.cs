using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed = 6f;
    public float range = 0.5f;
    public float detectionRange = 7f;
    public float followRange = 10f;
    public float waitTime = 2f;
    [Space]
    public float attackCoolDown = 1.2f;
    public float KnockBack = 1f;

    public Transform arrowPrefab;

    protected float startAttackCoolDown;
    protected Vector3[] points;
    protected NPCstate state;

    protected NPCPathfindingMovement NPCmovement;
    protected GameObject target;

    protected int currentHangingIndex = 0;
    protected bool _isStanding = false;

    public enum NPCstate
    {
        hanging = 0,
        chasing,
        attacking,
    }

    public virtual void Start()
    {
        NPCmovement = GetComponent<NPCPathfindingMovement>();
        NPCmovement.SetSpeed(speed);

        target = GameObject.FindGameObjectWithTag("Player");
        startAttackCoolDown = attackCoolDown;

        GeneratePoints();
    }

    public virtual void Update()
    {
        if (startAttackCoolDown > 0)
            startAttackCoolDown -= Time.deltaTime;
    }

    void FixedUpdate()
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

    public void StateAttack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > range)
            state = NPCstate.chasing;

        if (startAttackCoolDown <= 0)
        {
            Attack();
        }
    }

    public void StateHanging()
    {
        if (NPCmovement != null)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (Vector2.Distance(transform.position, points[i]) < 1f)
                    _isStanding = true;
            }

            if (!_isStanding)
            {
                HangOut(points);
            }
            else
            {
                StartCoroutine(waiter());
            }

            EnemyTrigger();
        }
    }

    public void StateChasing()
    {
        if (NPCmovement != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= range)
            {
                NPCmovement.StopMoving();
                state = NPCstate.attacking;
            }
            else
            {
                NPCmovement.MoveTo(target.transform.position);
            }

            Relax();
        }
    }
    public virtual void Attack()
    {

    }

    private void Relax()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > followRange)
        {
            NPCmovement.StopMoving();
            state = NPCstate.hanging;
            GeneratePoints();
        }
    }

    private void EnemyTrigger()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < detectionRange)
            state = NPCstate.chasing;
    }

    private void GeneratePoints()
    {
        points = new Vector3[Random.Range(3, 6)];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector3(Random.Range(transform.position.x - 3, transform.position.x + 3), Random.Range(transform.position.y - 3, transform.position.y + 3));
        }
    }

    public void HangOut(Vector3[] pathPoints)
    {
        if (currentHangingIndex >= pathPoints.Length)
        {
            currentHangingIndex = 0;
        }

        if (Vector3.Distance(transform.position, pathPoints[currentHangingIndex]) > 0.2f)
        {
            NPCmovement.MoveTo(pathPoints[currentHangingIndex]);
        }
        else
        {
            currentHangingIndex++;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(waitTime);
        _isStanding = false;
        HangOut(points);

    }
}
