using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public void SpawnEnemy(Transform enemy, int level)
    {
        enemy.GetComponent<EnemyStat>().level = level;
        Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    

}
