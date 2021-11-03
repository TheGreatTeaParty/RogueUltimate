using UnityEngine;
using Pathfinding;

public class Warrior : EnemyAI
{
    protected LayerMask _whatIsEnemy;
    protected Vector3 _direction;


    protected override void Start()
    {
        base.Start();
        _whatIsEnemy = LayerMask.GetMask("Player","EnvObjects");
    }

    protected override void Update()
    {
        base.Update();
        _direction = lastDirection;
    }

    protected override void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRadius, _whatIsEnemy);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if(enemiesToDamage[i]!= gameObject)
                enemiesToDamage[i].GetComponent<IDamaged>().
                    TakeDamage(stats.PhysicalDamage.Value, stats.MagicDamage.Value);
        }
        
        isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition, attackRadius);
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
