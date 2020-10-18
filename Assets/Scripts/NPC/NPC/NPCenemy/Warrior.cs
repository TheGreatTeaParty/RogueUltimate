using UnityEngine;

public class Warrior : EnemyAI
{
    private LayerMask _whatIsEnemy;
    private Vector2 _attackPosition;
    private Vector3 _direction;


    protected override void Start()
    {
        base.Start();
        _whatIsEnemy = LayerMask.GetMask("Player");
    }

    protected override void Update()
    {
        base.Update();
        _direction = lastDirection;
        _attackPosition = transform.position + _direction;
    }

    protected override void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRange, _whatIsEnemy);
        
        for (int i = 0; i < enemiesToDamage.Length; i++)
            enemiesToDamage[i].GetComponent<IDamaged>().
                TakeDamage(stats.PhysicalDamage.Value, stats.MagicDamage.Value);
        
        isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition, attackRange);
    }
    
}
