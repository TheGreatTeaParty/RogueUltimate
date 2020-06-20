using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed = 6f;
    public float range = 0.5f;
    public float waitTime = 2f;

    [Space]
    public Vector3[] points;
    public NPCstate state;

    private NPCPathfindingMovement NPCmovement;
    private Pathfinding pathfinding;
    private GameObject target;

    private int currentHangingIndex = 0;
    private bool _isStanding = false;

    public enum NPCstate
    {
        hanging = 0,
        chasing,
        attacking,
    }

    void Start()
    {
        NPCmovement = GetComponent<NPCPathfindingMovement>();
        NPCmovement.SetPathfinding(pathfinding);
        NPCmovement.SetSpeed(speed);

        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case NPCstate.chasing:
                {
                    if (pathfinding != null)
                    {
                        if (Vector2.Distance(transform.position, target.transform.position) <= range)
                            NPCmovement.StopMoving();

                        else
                        {
                            NPCmovement.MoveTo(target.transform.position);
                        }
                    }
                    break;
                }

            case NPCstate.hanging:
                {
                    if (pathfinding != null)
                    {
                        for (int i = 0; i < points.Length; i++)
                        {
                            if (Vector2.Distance(transform.position, points[i]) < 1f)
                                _isStanding = true;
                        }

                        if (!_isStanding)
                            HangOut(points);
                        else
                        {
                            StartCoroutine(waiter());
                        }
                    }
                    break;
                }
        }
    }

    public void SetPathfinding(Pathfinding pathfinding)
    {
        this.pathfinding = pathfinding;
    }

    public void HangOut(Vector3[] pathPoints)
    {
        if (currentHangingIndex >= pathPoints.Length)
        {
            currentHangingIndex = 0;
        }

        if (Vector3.Distance(transform.position, pathPoints[currentHangingIndex]) > 1f)
        {
            NPCmovement.MoveTo(pathPoints[currentHangingIndex]);
        }
        else
        {
            currentHangingIndex++;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(waitTime);
        _isStanding = false;
        HangOut(points);
    }
    IEnumerator waiterLast()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
