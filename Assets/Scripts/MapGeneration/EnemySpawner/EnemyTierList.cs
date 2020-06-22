using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTierList : MonoBehaviour
{
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

    public Transform GenerateEnemy()
    {
        return Enemies[Random.Range(0, Enemies.Length)];
    }
}
