using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AccountData
{
    public string scene;

    public float renown; //global money

    public int keeper_lvl, smith_lvl, master_lvl; //current proggress



    public AccountData()
    {
        if(!PlayerOnScene.Instance)
            scene = SceneManager.GetActiveScene().name;
        else
            scene = "Tavern";

        AccountManager accountManager = AccountManager.Instance;
        renown = accountManager.Renown;

        keeper_lvl = accountManager.KeeperLevel;
        smith_lvl = accountManager.SwithLevel;
        master_lvl = accountManager.MasterLevel;
    }
}
