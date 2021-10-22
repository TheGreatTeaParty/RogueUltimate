using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AccountData
{

    public float renown; //global money

    public int keeper_lvl, smith_lvl, master_lvl; //current proggress



    public AccountData()
    {
        AccountManager accountManager = AccountManager.Instance;
        renown = accountManager.Renown;

        keeper_lvl = accountManager.KeeperLevel;
        smith_lvl = accountManager.SwithLevel;
        master_lvl = accountManager.MasterLevel;
    }
}
