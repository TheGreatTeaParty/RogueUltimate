using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottom_rooms;
    public GameObject[] top_rooms;
    public GameObject[] left_rooms;
    public GameObject[] right_rooms;

    public GameObject closed_room;

    public List<GameObject> rooms;
    public float wait_time = 2f;
    public GameObject endpoint;

    private bool end_point_is_spawned = false;

    private void Update()
    {
        if(wait_time <= 0 && !end_point_is_spawned)
        {
            Instantiate(endpoint, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            end_point_is_spawned = true;
        }
        else if(!end_point_is_spawned)
        {
            wait_time -= Time.deltaTime;
        }
    }
}
