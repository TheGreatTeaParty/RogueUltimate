using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinding pathfinding = GetComponentInParent<SceneGrid>().GetPathfinding();
        Transform NPC = Instantiate(enemy, transform.position, Quaternion.identity);
        NPC.GetComponent<AI>().SetPathfinding(pathfinding);
        Destroy(gameObject);
    }
}
