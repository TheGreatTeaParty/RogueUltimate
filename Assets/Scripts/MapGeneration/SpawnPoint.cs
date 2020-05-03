using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    /*1 - top
      2 - bottom
      3 - left
      4 - right
    */
    public int oppenning_direction;

    private RoomTemplates templates;
    private int rand;
    private bool is_spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);  //Call method spawn every 0.1 seconds
    }

    void Spawn()
    {
        if (is_spawned == false)
        {

            if (oppenning_direction == 1)
            {
                //Spawn Room with Bottom door
                rand = Random.Range(0, templates.bottom_rooms.Length);
                Instantiate(templates.bottom_rooms[rand], transform.position, Quaternion.identity);
            }
            else if (oppenning_direction == 2)
            {
                //Spawn Room with top door
                rand = Random.Range(0, templates.top_rooms.Length);
                Instantiate(templates.top_rooms[rand], transform.position, Quaternion.identity);
            }
            else if (oppenning_direction == 3)
            {
                //Spawn Room with right door
                rand = Random.Range(0, templates.left_rooms.Length);
                Instantiate(templates.left_rooms[rand], transform.position, Quaternion.identity);
            }
            else if (oppenning_direction == 4)
            {
                //Spawn Room with left door
                rand = Random.Range(0, templates.right_rooms.Length);
                Instantiate(templates.right_rooms[rand], transform.position, Quaternion.identity);
            }
            is_spawned = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
            Destroy(gameObject);
    }
}
