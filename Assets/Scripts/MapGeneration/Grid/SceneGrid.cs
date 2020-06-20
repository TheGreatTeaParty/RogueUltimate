using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGrid : MonoBehaviour
{
    private Pathfinding pathfinding;

    // Start is called before the first frame update
    void Awake()
    {
        pathfinding = new Pathfinding(18, 18,transform.position, new Vector3(-9, -9, 0));
        pathfinding.RaycastWalkable();
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
}
