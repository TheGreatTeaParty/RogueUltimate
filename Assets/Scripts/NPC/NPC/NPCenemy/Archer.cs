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
        _shootDir = Vector3.Normalize(_playerCollider.bounds.center - _collider.bounds.center);
    }
    
    protected override void Attack()
    {
        Transform arrow = Instantiate(arrowPrefab, _collider.bounds.center + _shootDir, Quaternion.identity);

        arrow.GetComponent<FlyingObject>().SetData(stats.PhysicalDamage.Value, stats.MagicDamage.Value,_shootDir,false,0,null,false);
        isAttack = false;
        MoveRandomly();
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
        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, _shootDir, attackRange,LayerMask.GetMask("Player"));
        if (hit) return true;
        return false;
    }

    private void MoveRandomly()
    {
        Vector3 temp = transform.position - target.transform.position;
        temp = Quaternion.Euler(0,0, Random.Range(-90, 90)) * temp;
        Vector3 destination = target.transform.position + temp.normalized*(attackRange - Random.Range(0f,attackRange*0.5f));
        if (IsPositionAvailable(destination))
        {
            state = NPCstate.Hanging;
            followPosition = destination;
            Debug.DrawRay(_collider.bounds.center, destination, Color.red);
        }
    }

}
