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

    public float speed = 2f;
    public float pointStandingTime = 2f;

    [Space]

    public GameObject[] HangingPoints;

    //private NPCPathfindingMovement NPCmovement;

    private Vector3 startPosition;
    private int currentHangingIndex = 0;
    private bool _isStanding = false;
    private bool _isCalled;
    private TaverKeeperState STATE;

    public enum TaverKeeperState
    {
        standing = 0,
        hanging,
    };

    override public void Start()
    {
        _isCalled = true;
       // NPCmovement = GetComponent<NPCPathfindingMovement>();
        //NPCmovement.SetSpeed(speed);
        startPosition = transform.position;
    }


    override public void Update()
    {
        if (STATE == TaverKeeperState.hanging && Vector2.Distance(transform.position, startPosition) < 0.2f)
            STATE = TaverKeeperState.standing;
    }
    override public void FixedUpdate()
    {
        switch (STATE)
        {
            case TaverKeeperState.hanging:
                {

                    if (WaitForCall())
                        break;

                    if (currentHangingIndex < points.Length && Vector2.Distance(transform.position, points[currentHangingIndex]) < 0.2f)
                        _isStanding = true;

                    if (!_isStanding)
                        HangOut(HangingPoints);
                    else
                    {
                        StartCoroutine(waiter());
                    }

                    break;
                }

            case TaverKeeperState.standing:
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
            return true;
        }
        return false;
    }

    private void HangOut(GameObject[] pathPoints)
    {
        if (currentHangingIndex >= pathPoints.Length)
        {
            currentHangingIndex = 0;
        }

        if (Vector3.Distance(transform.position, pathPoints[currentHangingIndex].transform.position) > 0.2f)
        {
           // (pathPoints[currentHangingIndex]);
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
        HangOut(HangingPoints);
    }
    IEnumerator Stand()
    {
        yield return new WaitForSeconds(waitTime);
        state = NPCstate.hanging;
    }

    public void Interact()
    {
        ///Bind the info for TradeManager
        var playerInventory = InventoryManager.Instance;
        var npcInventory = GetComponent<NPCInventory>();
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerInventory, npcInventory);

        tradeManager.gold = playerInventory.GetGold();
        tradeManager.relation = npcInventory.relation;
    }

    public string GetActionName()
    {
        return "Trade";
    }
}
