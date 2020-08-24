using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private Transform enemy;

    public void SpawnAnEnemy()
    {
        
        Instantiate(enemy,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void SetEnemy(Transform _enemy)
    {
        enemy = _enemy;
    }

    public void SetlLvL(int lvl)
    {
        enemy.GetComponent<EnemyStat>().level = lvl;
    }
}
