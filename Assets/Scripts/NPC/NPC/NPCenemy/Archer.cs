using UnityEngine;

public class Archer : EnemyAI
{
    private Vector3 _shootDir;
    private Collider2D _playerCollider;

    public Transform arrowPrefab;
    public float RunBack = 2f;


    protected override void Start()
    {
        base.Start();
        _playerCollider = target.GetComponent<Collider2D>();
    }
    protected override void Update()
    {
        base.Update();
        _shootDir = Vector3.Normalize(target.transform.position - transform.position);
    }
    
    protected override void Attack()
    {
        Transform arrow = Instantiate(arrowPrefab, transform.position + _shootDir, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(stats.PhysicalDamage.Value, stats.MagicDamage.Value,_shootDir,false);
        isAttack = false;
    }
    protected override void StateChasing()
    {
        if (path == null) return;

        if (!isStopped)
            rb.MovePosition(transform.position + (Vector3)direction * movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange && CheckTarget())
            state = NPCstate.Attacking;
    }

    private bool CheckTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, (_playerCollider.bounds.center - _collider.bounds.center).normalized, attackRange,LayerMask.GetMask("Player"));
        if (hit) return true;
        return false;
    }


}
