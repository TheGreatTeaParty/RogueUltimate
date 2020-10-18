using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public void SpawnEnemy(Transform enemy, int level)
    {
        enemy.GetComponent<EnemyStat>().Level = level;
        Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    

}
