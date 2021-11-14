using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AccountManager : MonoBehaviour
{
    #region Singleton

    public static AccountManager Instance;
    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    private float _renown = 0;
    private int _keeper_level = 1, _smith_level = 1, _master_level = 1;

    public event Action<float> OnRenownChanged;

    public float Renown
    {
        get => _renown;
        set
        {
            _renown = value;
            OnRenownChanged?.Invoke(Renown);
        }
    }

    public int KeeperLevel
    {
        get => _keeper_level;
        set => _keeper_level = value;
    }

    public int SwithLevel
    {
        get => _smith_level;
        set => _smith_level = value;
    }

    public int MasterLevel
    {
        get => _master_level;
        set => _master_level = value;
    }

    public void IncreaseKeeperLevel()
    {
        _keeper_level++;
    }

    public void IncreaseSmithLevel()
    {
        _smith_level++;
    }

    public void IncreaseMasterLevel()
    {
        _master_level++;
    }

    public void LoadData(AccountData data)
    {
        Renown = data.renown;
        _keeper_level = data.keeper_lvl;
        _smith_level = data.smith_lvl;
        _master_level = data.master_lvl;
    }
}
