using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObj;
    private Vector3 SpawnPos;

    void Start()
    {
        SpawnPos = spawnObj.transform.position;
    }

   public Vector3 GetPosition()
    {
        return SpawnPos;
    }
}
