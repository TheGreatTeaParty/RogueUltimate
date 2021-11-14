using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using System;

public class TavernKeeperUpgrade : MonoBehaviour
{
    public static TavernKeeperUpgrade Instance;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        Instance = this;
    }
    private float Level2Price = 350;
    private float Level3Price = 1000;
    private float Level4Price = 1500;
    private float Level5Price = 2500;

    [Space]
    [HideInInspector]
    public int KeeperMaxLVL = 3;
    [HideInInspector]
    public int SmithMaxLVL = 3;
    [HideInInspector]
    private int MasterMaxLVL = 5;

    public event Action<TradeManager.tradeType> OnUpgraded;

    public GameObject TLVL1;
    public GameObject TLVL2;
    public GameObject TLVL3;
    [Space]
    public GameObject SLVL1;
    public GameObject SLVL2;
    public GameObject SLVL3;
    [Space]
    public GameObject MLVL1;
    public GameObject MLVL2;
    public GameObject MLVL3;

    private int _currentKeeperLevel;
    private int _currentSmithLevel;
    private int _currentMasterLevel;

    private AccountManager account;

    // Start is called before the first frame update
    void Start()
    {
        account = AccountManager.Instance;
        _currentKeeperLevel = account.KeeperLevel;
        _currentSmithLevel = account.SwithLevel;
        _currentMasterLevel = account.MasterLevel;
        TradeManager.Instance.OnTradeUpgraded += Upgrade;
        SetTavernLevel();
        SetSmithLevel();
        SetMasterLevel();
    }

    private void Upgrade(TradeManager.tradeType type)
    {
        switch (type)
        {
            case TradeManager.tradeType.tavernKeeper:
                {
                    UpgradeTavern();
                    break;
                }
            case TradeManager.tradeType.smith:
                {
                    UpgradeSmith();
                    break;
                }
            case TradeManager.tradeType.master:
                {
                    UpgradeMaster();
                    break;
                }
        }
        SaveManager.AccountAutoSave();
    }

    private void UpgradeTavern()
    {
        if (_currentKeeperLevel < KeeperMaxLVL)
        {
            account.Renown -= GetReqiredPrice(TradeManager.tradeType.tavernKeeper);
            _currentKeeperLevel++;
            account.IncreaseKeeperLevel();
            SetTavernLevel();
            OnUpgraded?.Invoke(TradeManager.tradeType.tavernKeeper);
            FirebaseAnalytics.LogEvent("tavern_upgrades",
                new Parameter("type", "keeper"));
        }
    }

    private void UpgradeSmith()
    {
        if (_currentSmithLevel < SmithMaxLVL)
        {
            account.Renown -= GetReqiredPrice(TradeManager.tradeType.smith);
            _currentSmithLevel++;
            account.IncreaseSmithLevel();
            SetSmithLevel();
            OnUpgraded?.Invoke(TradeManager.tradeType.smith);
            FirebaseAnalytics.LogEvent("tavern_upgrades",
               new Parameter("type", "smith"));
        }
    }

    private void UpgradeMaster()
    {
        if (_currentMasterLevel < MasterMaxLVL)
        {
            account.Renown -= GetReqiredPrice(TradeManager.tradeType.master);
            _currentMasterLevel++;
            account.IncreaseMasterLevel();
            SetMasterLevel();
            OnUpgraded?.Invoke(TradeManager.tradeType.master);
            FirebaseAnalytics.LogEvent("tavern_upgrades",
               new Parameter("type", "master"));
        }
    }

    private void SetTavernLevel()
    {
        switch(_currentKeeperLevel)
        {
            case 1:
                {
                    TLVL1.SetActive(true);
                    TLVL2.SetActive(false);
                    TLVL3.SetActive(false);

                    //Change the Tavern Keeper Inventory:
                    break;
                }
            case 2:
                {
                    TLVL1.SetActive(false);
                    TLVL2.SetActive(true);
                    TLVL3.SetActive(false);

                    //Change the Tavern Keeper Inventory:

                    break;
                }
            case 3:
                {
                    TLVL1.SetActive(false);
                    TLVL2.SetActive(false);
                    TLVL3.SetActive(true);

                    //Change the Tavern Keeper Inventory:
                    break;
                }

        }
    }

    private void SetSmithLevel()
    {
        switch (_currentSmithLevel)
        {
            case 1:
                {
                    SLVL1.SetActive(true);
                    SLVL2.SetActive(false);
                    SLVL3.SetActive(false);

                    //Change the Tavern Keeper Inventory:
                    break;
                }
            case 2:
                {
                    SLVL1.SetActive(false);
                    SLVL2.SetActive(true);
                    SLVL3.SetActive(false);

                    //Change the Tavern Keeper Inventory:

                    break;
                }
            case 3:
                {
                    SLVL1.SetActive(false);
                    SLVL2.SetActive(false);
                    SLVL3.SetActive(true);

                    //Change the Tavern Keeper Inventory:
                    break;
                }

        }
    }

    private void SetMasterLevel()
    {
        switch (_currentMasterLevel)
        {
            case 1:
                {
                    MLVL1.SetActive(true);
                    MLVL2.SetActive(false);
                    MLVL3.SetActive(false);

                    //Change the Tavern Keeper Inventory:
                    break;
                }
            case 2:
                {
                    MLVL1.SetActive(false);
                    MLVL2.SetActive(true);
                    MLVL3.SetActive(false);

                    //Change the Tavern Keeper Inventory:

                    break;
                }
            case 3:
                {
                    MLVL1.SetActive(false);
                    MLVL2.SetActive(false);
                    MLVL3.SetActive(true);

                    //Change the Tavern Keeper Inventory:
                    break;
                }
            default:
                break;
        }
    }
    public float GetReqiredPrice(TradeManager.tradeType type)
    {
        switch (type)
        {
            case TradeManager.tradeType.tavernKeeper:
                {
                    return GetPriceOnLevel(_currentKeeperLevel);
                }
            case TradeManager.tradeType.smith:
                {
                    return GetPriceOnLevel(_currentSmithLevel);
                }
            case TradeManager.tradeType.master:
                {
                    return GetPriceOnLevel(_currentMasterLevel);
                }
            default:
                return 0;
        }
    }
    private float GetPriceOnLevel(int level)
    {
        if (level == 1)
            return Level2Price;
        else if (level == 2)
            return Level3Price;
        else if (level == 3)
            return Level4Price;
        else if (level == 4)
            return Level5Price;
        return 0;
    }

    public bool IsMaxLevel(TradeManager.tradeType type)
    {
        switch (type)
        {
            case TradeManager.tradeType.tavernKeeper:
                {
                    if(KeeperMaxLVL <= _currentKeeperLevel)
                        return true;
                    return false;
                }
            case TradeManager.tradeType.smith:
                {
                    if (SmithMaxLVL <= _currentSmithLevel)
                        return true;
                    return false;
                }
            case TradeManager.tradeType.master:
                {
                    if (MasterMaxLVL <= _currentMasterLevel)
                        return true;
                    return false;
                }
            default:
                return true;
        }
    }

    public int GetCurrentLevel(TradeManager.tradeType type)
    {
        switch (type)
        {
            case TradeManager.tradeType.tavernKeeper:
                {
                    return _currentKeeperLevel;
                }
            case TradeManager.tradeType.smith:
                {
                    return _currentSmithLevel;
                }
            case TradeManager.tradeType.master:
                {
                    return _currentMasterLevel;
                }
            default:
                return 0;
        }
    }

}
