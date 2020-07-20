using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernKeeper : MonoBehaviour,IInteractable
{

    #region Singleton
    public static TavernKeeper Instance;
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

    //private NPCPathfindingMovement NPCmovement;

    private Vector3 startPosition;
    private int currentHangingIndex = 0;
    private bool _isStanding = false;
    private bool _isCalled;

    public enum NPCstate
    {
        hanging = 0,
        standing,
    }

    void Start()
    {
        _isCalled = true;
       // NPCmovement = GetComponent<NPCPathfindingMovement>();
        //NPCmovement.SetSpeed(speed);
        startPosition = transform.position;
    }


    private void Update()
    {
        if (state == NPCstate.hanging && Vector2.Distance(transform.position, startPosition) < 0.2f)
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

                    if (currentHangingIndex < points.Length && Vector2.Distance(transform.position, points[currentHangingIndex]) < 0.2f)
                        _isStanding = true;

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

    public void Call(bool option)
    {
        _isCalled = option;
    }

    private bool WaitForCall()
    {
        if (_isCalled)
        {
            //NPCmovement.MoveToTimer(startPosition);
            return true;
        }
        return false;
    }

    private void HangOut(Vector3[] pathPoints)
    {
        if (currentHangingIndex >= pathPoints.Length)
        {
            currentHangingIndex = 0;
        }

        if (Vector3.Distance(transform.position, pathPoints[currentHangingIndex]) > 0.2f)
        {
            //NPCmovement.MoveToTimer(pathPoints[currentHangingIndex]);
        }
        else
        {
            currentHangingIndex++;
        }
    }

    IEnumerator waiter()
    {
        //NPCmovement.StopMoving();
        yield return new WaitForSeconds(pointStandingTime);
        _isStanding = false;
        HangOut(points);
    }
    IEnumerator Stand()
    {
        yield return new WaitForSeconds(waitTime);
        state = NPCstate.hanging;
    }

    public void Interact()
    {
        if(_isCalled)
            Debug.Log("Dialog: Bla-Bla");
    }
}
