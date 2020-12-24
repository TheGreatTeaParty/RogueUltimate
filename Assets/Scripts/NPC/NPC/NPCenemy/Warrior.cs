using UnityEngine;
using Pathfinding;

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

    protected override void Die()
    {
        DestroyAllComponents();
        Destroy(this);
    }

    private void DestroyAllComponents()
    {
        Destroy(GetComponent<FloatingNumber>());
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Seeker>());
        Destroy(GetComponent<NPCAnimationHandler>());
        Destroy(GetComponent<DynamicLayerRenderer>());
        Destroy(GetComponent<AudioSource>());
    }
}
