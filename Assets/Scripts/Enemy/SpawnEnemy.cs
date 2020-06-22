using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private Transform enemy;

    public void SpawnAnEnemy()
    {
        enemy = EnemyTierList.Instance.GenerateEnemy();
        Pathfinding pathfinding = GetComponentInParent<SceneGrid>().GetPathfinding();
        Transform NPC = Instantiate(enemy, transform.position, Quaternion.identity);
        NPC.GetComponent<NPCPathfindingMovement>().SetPathfinding(pathfinding);
        Destroy(gameObject);
    }
}
