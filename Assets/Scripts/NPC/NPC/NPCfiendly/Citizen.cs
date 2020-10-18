using System.Collections;
using UnityEngine;

public enum CitizenState
{
    Hanging = 0,
    Standing,
}

public class Citizen : AI, IInteractable
{
    private DialogSystem _dialogSystem;
    [SerializeField]
    private Dialog dialog;

    public GameObject[] hangingPoints;

    [SerializeField] private float pointStandingTime = 3f;

    private int _currentHangingIndex = 0;
    private bool _coroutineHasStarted = false;
    private CitizenState _state;
    

    protected override void Start()
    {
        base.Start();
        _currentHangingIndex = Random.Range(0, hangingPoints.Length);
        target = hangingPoints[_currentHangingIndex];
        _dialogSystem = GetComponentInChildren<DialogSystem>();
    }

    public void FixedUpdate()
    {
        switch (_state)
        {
            case CitizenState.Hanging:
                {
                    //If the NPC reach the point
                    if (Vector2.Distance(transform.position, hangingPoints[_currentHangingIndex].transform.position) < 0.2f)
                         isStopped = true;

                    //If not, move it till the next point
                    if (!isStopped)
                        rb.MovePosition(rb.position + direction * movementSpeed * Time.deltaTime);
                    //If reached, run the standing timer
                    else
                        if (!_coroutineHasStarted)
                            StartCoroutine(Waiter());

                    break;
                }

            case CitizenState.Standing:
                break;
                
        }
        
    }

    public void Talk(bool option)
    {
        //If we talk, change to the standing
        if (option)
        {
            _state = CitizenState.Standing;
            isStopped = true;
        }

        //If not, change to the hanging
        else
        {
            _state = CitizenState.Hanging;
            isStopped = false;
        }
    }


    IEnumerator Waiter()
    {
        _coroutineHasStarted = true;
        yield return new WaitForSeconds(pointStandingTime);
        isStopped = false;

        //Move to another point
        if (_currentHangingIndex == hangingPoints.Length - 1)
        {
            _currentHangingIndex = 0;
        }
        else
        {
            _currentHangingIndex++;
        }
        target = hangingPoints[_currentHangingIndex];
        _coroutineHasStarted = false;
    }

    void IInteractable.Interact()
    {
        InterfaceManager.Instance.gameObject.SetActive(false);
        _dialogSystem.dialogWindow.SetActive(true);
        _dialogSystem.buttonContinue.SetActive(true);
        _dialogSystem.StartDialog(dialog);
        Talk(true);
    }

    string IInteractable.GetActionName() 
    {
        return "Talk";
    }
}   
