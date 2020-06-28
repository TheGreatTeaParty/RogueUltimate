using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AI
{
    public Transform arrowPrefab;

    private EnemyStat archerStat;
    public Vector3 c;
    public Vector3 e;
    public Vector3 dir;
    public override void Start()
    {
        base.Start();
        archerStat = GetComponent<EnemyStat>();
    }

    public override void Update()
    {
        base.Update();
        c = transform.position;
        e = target.transform.position;
        dir = (target.transform.position - transform.position)/ (target.transform.position - transform.position).magnitude;
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

    public override void Attack()
    {
        Transform arrow = Instantiate(arrowPrefab, transform.position + dir, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(archerStat.physicalDamage.GetValue(),archerStat.magicDamage.GetValue(),dir);
    }
}
