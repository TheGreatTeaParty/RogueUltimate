using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public enum TaverKeeperState
{
    Standing = 0,
    Hanging,
    GoingBack,
    Talking
}


public class TavernKeeper : AI, IInteractable
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


    private int _currentHangingIndex = 0;
    private bool _courantineHasStarted = false;
    private TaverKeeperState _state;
    
    public float pointStandingTime = 2f;
    [Space]
    public GameObject[] hangingPoints;
    public GameObject barPosition;
    [Space]
    public List<Item> List2;
    public List<Item> List3;

    private TavernKeeperUpgrade keeperUpgrade;
    private NPCInventory npcInventory;


    protected override void Start()
    {
        base.Start();
        if(barPosition)
            target = barPosition;
        TradeManager.Instance.OnTradeUpgraded += Upgrade;
        npcInventory = GetComponent<NPCInventory>();
        keeperUpgrade = TavernKeeperUpgrade.Instance;

        SetInvenotyOnStart();
    }

    public void FixedUpdate()
    {
        switch (_state)
        {
            case TaverKeeperState.Hanging:
                {
                    
                    if (Vector2.Distance(transform.position, hangingPoints[_currentHangingIndex].transform.position) < 0.2f)
                        isStopped = true;

                    if (!isStopped)
                        rb.MovePosition(rb.position + direction * movementSpeed * Time.deltaTime);
                    else
                        if (!_courantineHasStarted)
                            StartCoroutine(Waiter());
                    
                    break;
                }

            case TaverKeeperState.Standing:
                {
                    if (!_courantineHasStarted)
                        StartCoroutine(Stand());
                    
                    break;
                }
            
            case TaverKeeperState.GoingBack:
                {
                    rb.MovePosition(rb.position + direction * movementSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position,barPosition.transform.position) < 0.2f)
                        _state = TaverKeeperState.Standing;
                    break;
                }

            case TaverKeeperState.Talking:
                break;
        }
        
    }

    public void Call(bool option)
    {
        if (!option) return;
        
        _state = TaverKeeperState.GoingBack;
        target = barPosition;
    }

    private IEnumerator Waiter()
    {
        _courantineHasStarted = true;
        yield return new WaitForSeconds(pointStandingTime);
        isStopped = false;

        //Move to another point
        if (_currentHangingIndex == hangingPoints.Length - 1)
            _currentHangingIndex = 0;
        else
            _currentHangingIndex++;
        
        target = hangingPoints[_currentHangingIndex];
        _courantineHasStarted = false;
    }

    private IEnumerator Stand()
    {
        _courantineHasStarted = true;
        isStopped = true;
        yield return new WaitForSeconds(waitTime);
        _state = TaverKeeperState.Hanging;
        isStopped = false;
        target = hangingPoints[_currentHangingIndex];
        _courantineHasStarted = false;
    }

    public void Talk(bool option)
    {
        //If we talk, change to the standing
        if (option)
        {
            _state = TaverKeeperState.Talking;
            isStopped = true;
        }

        //If not, change to the hanging
        else
        {
            _state = TaverKeeperState.Standing;
        }
    }

    public void Interact()
    {
        // Bind the info for TradeManager
        var playerInventory = CharacterManager.Instance.Inventory;
        var tradeManager = TradeManager.Instance;

        tradeManager.Bind(playerInventory, npcInventory);
        tradeManager.Open(TradeManager.tradeType.tavernKeeper);
        Talk(true);
    }

    public string GetActionName()
    {
        return "Trade";
    }

    public void Upgrade(TradeManager.tradeType type)
    {
        if( type == TradeManager.tradeType.tavernKeeper)
        {
            if(keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.tavernKeeper) == 2)
            {
                npcInventory.items = List2;
                Interact();
            }
            else
            {
                npcInventory.items = List3;
                Interact();
            }
        }
    }
    public void SetInvenotyOnStart()
    {
        if (keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.tavernKeeper) == 2)
            npcInventory.items = List2;
        else if(keeperUpgrade.GetCurrentLevel(TradeManager.tradeType.tavernKeeper) == 3)
            npcInventory.items = List3;
    }
    
}
