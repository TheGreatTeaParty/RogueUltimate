﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerEnterCheck : MonoBehaviour
{
    private EnemySpawner _eSpawner;
    private EnemySpawnPoint[] _eSpawnPoints;
    private bool _isSpawned;

    
    private void Start()
    {
        _eSpawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
        _eSpawner = GetComponentInChildren<EnemySpawner>();
        _isSpawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        if (_isSpawned) 
            return;
        
        var playerLevel = collision.GetComponent<PlayerStat>().level;
        _eSpawner.SetPlayerLevel(playerLevel);

        int enemyLevel = _eSpawner.GetEnemyLevel();
        for (int i = 0; i < _eSpawnPoints.Length; i++)
            _eSpawnPoints[i].SpawnEnemy(_eSpawner.GetEnemy(), enemyLevel);

        _isSpawned = true;
        
    }
    
    
}
