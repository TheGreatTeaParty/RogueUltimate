using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newposition = player.transform.position;
        newposition.z = transform.position.z;
        transform.position = newposition;
    }
}
