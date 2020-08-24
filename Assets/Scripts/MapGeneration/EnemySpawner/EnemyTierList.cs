﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTierList : MonoBehaviour
{
    [SerializeField] private int Enemies_Level;
    [Space]

    [SerializeField] private Transform[] Enemies;

    #region Singleton
    public static EnemyTierList Instance;
    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    #endregion
    /// <summary>
    /// Some chnges in this script bla-bla-bla
    /// </summary>
    /// <returns></returns>
    public Transform GenerateEnemy()
    {
        return Enemies[Random.Range(0, Enemies.Length)];
    }

    public int GetEnemieslvl()
    {
        return Enemies_Level;
    }
}
