using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warior : AI
{
    public float AttachRange;

    private LayerMask whatIsEnemy;
    private Vector2 attackPosition;
    private Vector3 direction;

    private EnemyStat WariorStat;

    public override void Start()
    {
        base.Start();
        WariorStat = GetComponent<EnemyStat>();
        whatIsEnemy = LayerMask.GetMask("Player");
    }

    public override void Update()
    {
        base.Update();
        attackPosition = transform.position + direction;
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
        direction = NPCmovement.GetLastMoveDir();
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, AttachRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(direction * 10 * KnockBack,ForceMode2D.Impulse);
                enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(WariorStat.physicalDamage.GetValue(), WariorStat.magicDamage.GetValue());
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, AttachRange);
    }
}
