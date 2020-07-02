﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCheck : MonoBehaviour
{
    private SpawnEnemy[] SpawnEnemyPoints;
    private bool _isSpawned = false;

    private void Start()
    {
        SpawnEnemyPoints = GetComponentsInChildren<SpawnEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!_isSpawned)
            {
                _isSpawned = true;
                for (int i = 0; i < SpawnEnemyPoints.Length; i++)
                    SpawnEnemyPoints[i].SpawnAnEnemy();
            }
        }

    }
}