using System.Collections.Generic;
using UnityEngine;


public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottom_rooms;
    public GameObject[] top_rooms;
    public GameObject[] left_rooms;
    public GameObject[] right_rooms;
    public GameObject[] close_rooms;

    public List<GameObject> rooms;
    public float wait_time = 2f;
    public int max_rooms;
    public GameObject endpoint;
    public LevelManager.Scenes scenes;

    [Space]
    [SerializeField] private bool SpawnDwarf = false;
    [SerializeField] private GameObject DwarfSeller;

    private bool end_point_is_spawned = false;
    private GameObject Portal;

    private void Update()
    {
        if(wait_time <= 0 && !end_point_is_spawned)
        {
            Portal = Instantiate(endpoint, rooms[rooms.Count - 1].GetComponent<PortalSpawnPoint>().GetPosition(), Quaternion.identity);
            Portal.GetComponent<NextLevelTrigger>().SetNextLevel(scenes);
            rooms[rooms.Count - 1].GetComponentInChildren<PlayerEnterCheck>().gameObject.SetActive(false);
            end_point_is_spawned = true;

            if (SpawnDwarf)
            {
                int number = Random.Range(1, rooms.Count - 1);
                Instantiate(DwarfSeller, rooms[number].transform.position, Quaternion.identity);
                rooms[number].GetComponentInChildren<PlayerEnterCheck>().gameObject.SetActive(false);
            }
        }
        else if(!end_point_is_spawned)
        {
            wait_time -= Time.deltaTime;
        }
        
        //Deleting rooms from the list if it has been distroyed
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i] == null)
            {
                rooms.Remove(rooms[i]);
            }
        }
    }
}
