using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Warrior
{

    public float RunBack = 2f;
    private bool _started = false;

    protected override void Attack()
    {
        base.Attack();
        state = NPCstate.Hanging;
        Vector2 direction = (target.transform.position - transform.position);
        Vector2 runBack = transform.position + (Vector3)(-direction.normalized * RunBack);
        GenerateFollowPosition(runBack);
    }

    protected override void StateHanging()
    {
        base.StateHanging();
        if (path != null)
        {
            if(Vector2.Distance(transform.position,followPosition) > 0.1f)
                rb.MovePosition(transform.position + (Vector3)direction * movementSpeed * Time.deltaTime);
            else
            {
                if (!_started)
                    StartCoroutine(EnemyWait());
            }
        }
    }


    protected IEnumerator EnemyWait()
    {
        _started = true;
        state = NPCstate.Chasing;
        direction = nextPointDir;
        StopMoving();
        yield return new WaitForSeconds(waitTime);
        StartMoving();
        _started = false;
    }
}
