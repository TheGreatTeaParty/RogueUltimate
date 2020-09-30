using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernKeeper : AI,IInteractable
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

    public float pointStandingTime = 2f;

    [Space]

    public GameObject[] HangingPoints;
    public GameObject BarPosition;

    //private NPCPathfindingMovement NPCmovement;

    private int currentHangingIndex = 0;
    private bool courantineHasStarted = false;
    private TaverKeeperState STATE;


    public enum TaverKeeperState
    {
        standing = 0,
        hanging,
        goingBack,
        talking

    };

    override public void Start()
    {
        base.Start();
        target = BarPosition;
    }


    override public void Update()
    {
        base.Update();
    }

    override public void FixedUpdate()
    {
        switch (STATE)
        {
            case TaverKeeperState.hanging:
                {
                    
                    if (Vector2.Distance(transform.position, HangingPoints[currentHangingIndex].transform.position) < 0.2f)
                        _stopped = true;

                    if (!_stopped)
                    {
                        Rb.MovePosition(Rb.position + dir * Speed * Time.deltaTime);
                    }
                    else
                    {

                        if(!courantineHasStarted)
                            StartCoroutine(waiter());
                    }

                    break;
                }

            case TaverKeeperState.standing:
                {
                    if (!courantineHasStarted)
                        StartCoroutine(Stand());
                    break;
                }
            case TaverKeeperState.goingBack:
                {
                    Rb.MovePosition(Rb.position + dir * Speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position,BarPosition.transform.position) < 0.2f)
                        STATE = TaverKeeperState.standing;
                    break;
                }

            case TaverKeeperState.talking:
                {
                    break;
                }
        }
    }

    public void Call(bool option)
    {
        if (option)
        {
            STATE = TaverKeeperState.goingBack;
            target = BarPosition;
        }
    }


    IEnumerator waiter()
    {
        courantineHasStarted = true;
        yield return new WaitForSeconds(pointStandingTime);
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
    IEnumerator Stand()
    {
        courantineHasStarted = true;
        _stopped = true;
        yield return new WaitForSeconds(waitTime);
        STATE = TaverKeeperState.hanging;
        _stopped = false;
        target = HangingPoints[currentHangingIndex];
        courantineHasStarted = false;
    }

    public void Talk(bool option)
    {
        //If we talk, change to the standing
        if (option)
        {
            STATE = TaverKeeperState.talking;
            _stopped = true;
        }

        //If not, change to the hanging
        else
        {
            STATE = TaverKeeperState.standing;
        }
    }

    public void Interact()
    {
        // Bind the info for TradeManager
        var playerInventory = CharacterManager.Instance;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerInventory, npcInventory);
        tradeManager.Open();
        Talk(true);
    }

    public string GetActionName()
    {
        return "Trade";
    }
}
