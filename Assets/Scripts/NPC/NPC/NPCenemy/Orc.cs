using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Warrior
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override IEnumerator AttackWait()
    {
        _attackPosition = transform.position + (target.transform.position - transform.position).normalized * attackRange;
        isAttack = true;
        StopMoving();
        animator.SetTrigger("Attack_State");
        yield return new WaitForSeconds(attackCoolDown);
        OnAttacked?.Invoke();
        yield return new WaitForSeconds(attackDuration);
        state = NPCstate.Chasing;
        Attack();
        StartMoving();
    }
}
