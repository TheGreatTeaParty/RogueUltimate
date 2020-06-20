using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathfindingMovement : MonoBehaviour {

    private float SPEED;

    private Pathfinding pathfinding;
    private Rigidbody2D enemyRb;
    private List<Vector3> pathVectorList;
    private int currentPathIndex;
    private float pathfindingTimer;
    private Vector2 moveDir;
    private Vector3 lastMoveDir;

    private void Awake() {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        pathfindingTimer -= Time.deltaTime;

        HandleMovement();
    }

    private void FixedUpdate() {
        enemyRb.MovePosition(enemyRb.position + moveDir * SPEED * Time.fixedDeltaTime);
    }

    private void HandleMovement()
    {
        PrintPathfindingPath();
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
          
            if (Vector3.Distance(GetPosition(), targetPosition) > 1f)
            {
                moveDir = (targetPosition - GetPosition()).normalized;
                lastMoveDir = moveDir;
            }

            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                
                }
            }
        }
    }

    public void StopMoving() {
        pathVectorList = null;
        moveDir = Vector3.zero;
    }

    public List<Vector3> GetPathVectorList() {
        return pathVectorList;
    }

    private void PrintPathfindingPath() {
        if (pathVectorList != null) {
            for (int i=0; i<pathVectorList.Count - 1; i++) {
                Debug.DrawLine(pathVectorList[i], pathVectorList[i + 1],Color.green);
            }
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        SetTargetPosition(targetPosition);
    }

    public void MoveToTimer(Vector3 targetPosition)
    {
        if (pathfindingTimer <= 0f)
        {
            SetTargetPosition(targetPosition);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;

        pathVectorList = pathfinding.FindPath(GetPosition(), targetPosition);
        pathfindingTimer = .2f;

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    public void SetPathfinding(Pathfinding pathfinding)
    {
        this.pathfinding = pathfinding;
    }

    public void SetSpeed(float speed)
    {
        this.SPEED = speed;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetLastMoveDir()
    {
        return lastMoveDir;
    }      
    
    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        enemyRb.velocity = Vector3.zero;
    }

}
