using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGrid : MonoBehaviour
{
    public float GridSize;
    public int x;
    public int y;
    [Space]
    public int XOffSet;
    public int YOffSet;

    private Pathfinding pathfinding;

    // Start is called before the first frame update
    void Awake()
    {
        pathfinding = new Pathfinding(x, y,transform.position, new Vector3(XOffSet, YOffSet, 0),GridSize);
        pathfinding.RaycastWalkable();
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
}
