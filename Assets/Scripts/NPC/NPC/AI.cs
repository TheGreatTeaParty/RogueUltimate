using UnityEngine;
using Pathfinding;


public enum NPCstate
{
    Chasing = 0,
    Attacking,
    Hanging
}


public class AI : MonoBehaviour
{
    [SerializeField] protected float nextWayPointDistance = 1f;
    [SerializeField] protected float movementSpeed = 4f;
    [SerializeField] protected float detectionRange = 7f;
    [SerializeField] protected float followRange = 10f;
    [SerializeField] protected float waitTime = 2f;
    [Space]
    [SerializeField] protected float attackCoolDown = 1.2f;
    [SerializeField] protected float attackDuration = 0.5f;

    protected Vector3[] points;
    protected NPCstate state;
    protected int currentPathPoint;
    protected bool reachedEnd = false;
    protected Vector2 direction;
    protected Vector2 lastDirection;
    protected GameObject target;

    protected Path path;
    protected Seeker seeker;
    protected Rigidbody2D rb;
    protected bool isStopped = false;
    protected bool isAttack = false;


    public delegate void OnAttackedEvent();
    public OnAttackedEvent OnAttacked;

    
    protected virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }
    
    protected virtual void Update()
    {
        if (path == null)
            return;

        if (currentPathPoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }

        reachedEnd = false;
        direction = (path.vectorPath[currentPathPoint] - transform.position).normalized;

        if (direction != Vector2.zero)
            lastDirection = direction;

        if (Vector2.Distance(rb.position, path.vectorPath[currentPathPoint]) < nextWayPointDistance)
            currentPathPoint++;
        
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentPathPoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
    }

    protected virtual void StateChasing()
    {
        if (path == null) return;

        if (!isStopped)
                rb.MovePosition(transform.position + (Vector3) direction * movementSpeed * Time.deltaTime);
    }

    protected void StopMoving()
    {
        isStopped = true;
    }

    protected void StartMoving()
    {
        isStopped = false;
    }

    protected virtual void Die()
    {
        Destroy(this);
    }
    
    public  virtual Vector2 GetDirection()
    {
        return direction;
    }
    
    public Vector2 GetVelocity()
    {
        return isStopped ? new Vector2(0f, 0f) : direction;
    }
}