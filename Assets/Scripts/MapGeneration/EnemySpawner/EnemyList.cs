using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField] private Transform[] firstTierEnemies;
    [SerializeField] private Transform[] secondTierEnemies;
    [SerializeField] private Transform[] thirdTierEnemies;

    
    public Transform GenerateFirstTierEnemy()
    {
        return firstTierEnemies[Mathf.FloorToInt(Random.Range(0, firstTierEnemies.Length))];
    }
    
    public Transform GenerateSecondTierEnemy()
    {
        return secondTierEnemies[Mathf.FloorToInt(Random.Range(0, secondTierEnemies.Length))];
    }
    
    public Transform GenerateThirdTierEnemy()
    {
        return thirdTierEnemies[Mathf.FloorToInt(Random.Range(0, thirdTierEnemies.Length))];
    }


}
