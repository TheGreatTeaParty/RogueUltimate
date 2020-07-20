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
       // direction = NPCmovement.GetLastMoveDir();
        attackPosition = transform.position + direction;
    }

    public override void Attack()
    {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, AttachRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(WariorStat.physicalDamage.GetValue(), WariorStat.magicDamage.GetValue());
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, AttachRange);
    }
}
