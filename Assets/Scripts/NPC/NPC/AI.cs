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
    [SerializeField] public float movementSpeed = 4f;
    [SerializeField] protected float detectionRange = 7f;
    [SerializeField] protected float followRange = 10f;
    [SerializeField] protected float waitTime = 2f;
    [Space]
    [SerializeField] public float attackCoolDown = 1.2f;
    [SerializeField] protected float attackDuration = 0.5f;
    public float colliderDetactionRadius = 0.3f;

    protected Vector3[] points;
    protected NPCstate state;
    protected int currentPathPoint;
    protected bool reachedEnd = false;
    protected Vector2 nextPointDir;
    protected Vector2 direction;
    protected Vector2 reactionDirection = Vector2.zero;
    protected Vector2 lastDirection;
    protected GameObject target;

    protected Path path;
    protected Seeker seeker;
    protected Rigidbody2D rb;
    protected bool isStopped = false;
    protected bool isAttack = false;

    private LayerMask layerMask;
    private Collider2D _collider;

    public delegate void OnAttackedEvent();
    public OnAttackedEvent OnAttacked;

    public bool IsStopped => isStopped;
    
    protected virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        layerMask = LayerMask.GetMask("Player", "Enemy", "Wall");
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        InvokeRepeating("RayCastToDirection", 0f, 0.3f);
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
        nextPointDir = (path.vectorPath[currentPathPoint] - transform.position).normalized;

        if (direction != Vector2.zero)
            lastDirection = direction;

        if (Vector2.Distance(rb.position, path.vectorPath[currentPathPoint]) < nextWayPointDistance)
            currentPathPoint++;
    }
  
    private void RayCastToDirection()
    {
        int angle = 120;
        float sum = 0;
        int number = 0;
        for(int i = 0; i < 9; i++)
        {
            Vector2 detactRay = Quaternion.AngleAxis(angle, Vector3.forward) * nextPointDir;
            RaycastHit2D ray = Physics2D.Raycast(_collider.bounds.center, detactRay, colliderDetactionRadius);
            Debug.DrawRay(_collider.bounds.center, detactRay*colliderDetactionRadius, Color.green);
            if(ray)
            {
                Debug.DrawRay(_collider.bounds.center, detactRay * colliderDetactionRadius, Color.red);
                if (angle >= 0)
                    sum += (angle - 180);
                else
                    sum += (angle + 180);
                number++;
            }

            angle -= 30;
        }
        if(number == 0)
            direction = nextPointDir;
        else
        {
            direction = (Quaternion.AngleAxis(sum/number, Vector3.forward) * nextPointDir).normalized;
            Debug.DrawRay(_collider.bounds.center, direction, Color.gray);
        }
       
    }

    protected void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentPathPoint = 0;
        }
    }

    protected virtual void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
    }

    protected virtual void StateChasing()
    {
        if (path == null) return;

        if (!isStopped)
            rb.MovePosition(transform.position + (Vector3)direction * movementSpeed * Time.deltaTime);
    }

    public void StopMoving()
    {
        isStopped = true;
    }

    public void StartMoving()
    {
        isStopped = false;
    }

    public void ModifyMovementSpeed(float percent)
    {
        movementSpeed *= percent;
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