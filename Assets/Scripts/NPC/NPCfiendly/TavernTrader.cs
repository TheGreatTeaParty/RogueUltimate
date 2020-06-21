using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernTrader : MonoBehaviour
{

    #region Singleton
    public static TavernTrader Instance;
    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    #endregion

    public float speed = 2f;
    public float pointStandingTime = 2f;
    public float waitTime = 10f;

    [Space]

    public Vector3[] points;
    public NPCstate state;

    private NPCPathfindingMovement NPCmovement;

    private Vector3 startPosition;
    private int currentHangingIndex = 0;
    private bool _isStanding = false;
    private bool _isCalled = false;

    public enum NPCstate
    {
        hanging = 0,
        standing,
    }

    void Start()
    {
        NPCmovement = GetComponent<NPCPathfindingMovement>();
        NPCmovement.SetSpeed(speed);
        startPosition = transform.position;
    }


    private void Update()
    {
        if (state == NPCstate.hanging && Vector2.Distance(transform.position, startPosition) < 0.7f)
            state = NPCstate.standing;
    }
    void FixedUpdate()
    {
        switch (state)
        {
            case NPCstate.hanging:
                {

                    if (WaitForCall())
                        break;

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

                    break;
                }

            case NPCstate.standing:
                {
                    StartCoroutine(Stand());
                    break;
                }
        }
    }

    public bool WaitForCall()
    {
        if (_isCalled)
        {
            NPCmovement.MoveToTimer(startPosition);
            return true;
        }
        return false;
    }

    public void HangOut(Vector3[] pathPoints)
    {
        if (currentHangingIndex >= pathPoints.Length)
        {
            currentHangingIndex = 0;
            _isCalled = true;
        }

        if (Vector3.Distance(transform.position, pathPoints[currentHangingIndex]) > 1f)
        {
            NPCmovement.MoveToTimer(pathPoints[currentHangingIndex]);
        }
        else
        {
            currentHangingIndex++;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(pointStandingTime);
        _isStanding = false;
        HangOut(points);
    }
    IEnumerator Stand()
    {
        yield return new WaitForSeconds(waitTime);
        state = NPCstate.hanging;
    }
}
