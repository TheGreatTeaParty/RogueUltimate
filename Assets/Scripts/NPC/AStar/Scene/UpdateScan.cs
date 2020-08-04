using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UpdateScan : MonoBehaviour
{
    AstarPath astarPath;
    // Start is called before the first frame update
    void Start()
    {
        astarPath = GetComponent<AstarPath>();
        Invoke("Scanner", 2f);
    }

   
    void Scanner()
    {
        astarPath.Scan();
    }
}
