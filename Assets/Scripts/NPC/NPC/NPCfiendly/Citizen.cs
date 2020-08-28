using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Citizen : AI, IInteractable
{
    [SerializeField]
    private DialogSystem dialogSystem;
    [SerializeField]
    private GameObject dialogWindow;
    [SerializeField]
    private GameObject buttonContinue;
    [SerializeField]
    private Dialog dialog;

    public GameObject[] HangingPoints;

    [SerializeField] private float PointStandingTime = 3f;

    private int currentHangingIndex = 0;
    private bool courantineHasStarted = false;
    private CitizenState STATE;

    public enum CitizenState
    {
        hanging = 0,
        standing,
    };

    override public void Start()
    {
        base.Start();
        currentHangingIndex = Random.Range(0, HangingPoints.Length);
        target = HangingPoints[currentHangingIndex];
    }


    override public void Update()
    {
        base.Update();
    }

    override public void FixedUpdate()
    {
        switch (STATE)
        {
            case CitizenState.hanging:
                {
                    //If the NPC reach the point
                    if (Vector2.Distance(transform.position, HangingPoints[currentHangingIndex].transform.position) < 0.2f)
                         _stopped = true;

                    //If not, move it till the next point
                    if (!_stopped)
                    {
                        Rb.MovePosition(Rb.position + dir * Speed * Time.deltaTime);
                    }

                    //If reached, run the standing timer
                    else
                    {

                        if (!courantineHasStarted)
                            StartCoroutine(waiter());
                    }

                    break;
                }

            case CitizenState.standing:
                {
                    break;
                }
        }
    }

    public void Talk(bool option)
    {
        //If we talk, change to the standing
        if (option)
        {
            STATE = CitizenState.standing;
        }

        //If not, change to the hanging
        else
        {
            STATE = CitizenState.hanging;
        }
    }


    IEnumerator waiter()
    {
        courantineHasStarted = true;
        yield return new WaitForSeconds(PointStandingTime);
        _stopped = false;

        //Move to another point
        if (currentHangingIndex == HangingPoints.Length - 1)
        {
            currentHangingIndex = 0;
        }
        else
        {
            currentHangingIndex++;
        }
        target = HangingPoints[currentHangingIndex];
        courantineHasStarted = false;
    }

    void IInteractable.Interact()
    {
        InterfaceOnScene.instance.gameObject.SetActive(false);
        dialogWindow.SetActive(true);
        buttonContinue.SetActive(true);
        dialogSystem.StartDialog(dialog);
        Talk(true);
    }

    string IInteractable.GetActionName() 
    {
        return "Talk";
    }
}   
