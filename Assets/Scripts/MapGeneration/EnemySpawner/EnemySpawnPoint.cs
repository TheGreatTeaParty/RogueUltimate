using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public void SpawnEnemy(Transform enemy, int level)
    {
        Transform body = Instantiate(enemy, transform.position, Quaternion.identity);
        body.GetComponent<EnemyStat>().SetLevel(level);
        Destroy(gameObject);
    }
    

}
