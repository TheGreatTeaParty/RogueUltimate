using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AI
{
    
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
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
        Transform arrow = Instantiate(arrowPrefab, transform.position + (target.transform.position - transform.position).normalized, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(10, 1, (target.transform.position - transform.position).normalized);
        startAttackCoolDown = attackCoolDown;
    }
}
