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
    private bool _isStanding = false;
    private bool courantineHasStarted = false;
    private TaverKeeperState STATE;

    public enum TaverKeeperState
    {
        standing = 0,
        hanging,
        goingBack,

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
                        _isStanding = true;

                    if (!_isStanding)
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
        _isStanding = false;

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
        yield return new WaitForSeconds(waitTime);
        STATE = TaverKeeperState.hanging;
        target = HangingPoints[currentHangingIndex];
        courantineHasStarted = false;
    }

    public void Interact()
    {
        ///Bind the info for TradeManager
        var playerInventory = InventoryManager.Instance;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerInventory, npcInventory);

        tradeManager.gold = playerInventory.GetGold();
        tradeManager.relation = npcInventory.GetRelation();
    }

    public string GetActionName()
    {
        return "Trade";
    }
}
