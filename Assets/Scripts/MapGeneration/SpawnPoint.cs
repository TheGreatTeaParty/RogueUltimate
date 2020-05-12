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
    public float destroy_time = 4f;
    public bool has_door = false;
    public LayerMask layerMask;
    public bool is_empty = true;

    private float wait_time = 0.1f;
    private RoomTemplates templates;
    private int rand;
    private bool is_spawned = false;

    private void Start()
    {
        Destroy(gameObject, destroy_time);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        layerMask = LayerMask.GetMask("Default");
    }


    void Spawn()
    {
        //If there is more rooms than we need
        if (is_spawned == false && templates.rooms.Count <= templates.max_rooms - 1)
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
        else if(templates.rooms.Count > templates.max_rooms - 1)
        {
            //Delete extra rooms and force it spawn again.
            turn_on_spawn();
            Destroy(transform.parent.gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("SpawnPoint"))
        {
            if (collision.GetComponent<SpawnPoint>().is_spawned == false && is_spawned == false)
            {
                turn_on_spawn();
                Destroy(transform.parent.gameObject);
            }
            if (collision.GetComponent<SpawnPoint>().is_spawned == true && is_spawned == false)
            {
                if (has_door == false) //if there is room, which is not connected to our room ->destroy it
                {
                    turn_on_spawn();
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            is_spawned = true;
        }
    }

    private void turn_on_spawn()
    {
        Collider2D[] room = Physics2D.OverlapCircleAll(transform.parent.position,1f,layerMask);
        if (room.Length > 0)
        {
            for(int i = 0; i < room.Length; i++)
            {
                if(room[i].tag == "SpawnPoint")
                {
                    room[i].GetComponent<SpawnPoint>().is_empty = true;
                    break;
                }
            }
        }
    }

    private void Update()
    {

        if (wait_time <= 0)
        {
            EmptyRoom();
            wait_time = 0.1f;
        }
        else 
        {
            wait_time -= Time.deltaTime;
        }
    }
    private void EmptyRoom()
    {

        if (is_empty)
        {
            is_empty = false;
            is_spawned = false;

            Spawn();
        }
    }
}
