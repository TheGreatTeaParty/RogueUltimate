using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    //Adding rooms to the list
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
    
}
