using System.Collections;
using UnityEngine;

public enum CitizenState
{
    Hanging = 0,
    Standing,
}

public class Citizen : AI, IInteractable,ITalkable
{
    [SerializeField]
    protected Transform DialogueUI;
    [Space]
    public GameObject[] hangingPoints;

    [SerializeField] protected float pointStandingTime = 3f;

    private int _currentHangingIndex = 0;
    private bool _coroutineHasStarted = false;
    private CitizenState _state;
    protected Sprite _sprite;

    [Space]
    [SerializeField]
    protected DialogSystem.ECharacterNames characterName;
    [SerializeField]
    protected int PhrasesInSpeech = 1;

    protected override void Start()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
        base.Start();
        if (hangingPoints.Length > 0)
        {
            _currentHangingIndex = Random.Range(0, hangingPoints.Length);
            target = hangingPoints[_currentHangingIndex];
        }
    }

    public virtual void FixedUpdate()
    {
        switch (_state)
        {
            case CitizenState.Hanging:
                {
                    if (hangingPoints.Length < 1) { _state = CitizenState.Standing; break; }
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

    public virtual void Talk(bool option)
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
        Talk(true);
    }

    string IInteractable.GetActionName() 
    {
        return "Talk";
    }

    public void Talk()
    {
        var UI = Instantiate(DialogueUI);
        UI.GetComponent<DialogueUI>().Init(_sprite, characterName, PhrasesInSpeech);
    }
}   
