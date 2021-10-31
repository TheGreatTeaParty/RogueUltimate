using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalRoom : MonoBehaviour
{
    public int id;
    public RoomType roomType = RoomType.Normal;
    public Door[] doors;

    public Door.Direction NextDoorDir = Door.Direction.None;
    public Door.Direction PrevDoorDir = Door.Direction.None;

    private int doorIndex;
    private LayerMask layerMask;
    private PhysicalRoom prevRoom;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        doors = GetComponentsInChildren<Door>();
        layerMask = LayerMask.GetMask("Door");
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool GenerateNextDoor()
    {
        if(PrevDoorDir == Door.Direction.None)
        {
            doorIndex = Random.Range(0, doors.Length);
            NextDoorDir = doors[doorIndex].direction;
            return true;
        }

        else
        {
            //Genearate random door to be assigned
            List<Door> temp = new List<Door>(doors);

            for(int i = 0; i < doors.Length; i++)
            {
                doorIndex = Random.Range(0, doors.Length);
                if (doors[doorIndex].direction != PrevDoorDir && doors[doorIndex].direction!= NextDoorDir)
                {
                    NextDoorDir = doors[doorIndex].direction;
                    return true;
                }
                temp.Remove(doors[doorIndex]);
            }
            if (temp.Count != 0)
            {
                NextDoorDir =  temp[Random.Range(0, temp.Count)].direction;
                return true;
            }

            return false;
        }
    }

    public void SetParent(Door.Direction direction)
    {
        if (direction == Door.Direction.Top)
            PrevDoorDir = Door.Direction.Bot;

        else if (direction == Door.Direction.Bot)
            PrevDoorDir = Door.Direction.Top;

        else if(direction == Door.Direction.Right)
            PrevDoorDir = Door.Direction.Left;

        else if(direction == Door.Direction.Left)
            PrevDoorDir = Door.Direction.Right;
    }

    public Vector3 GetNextRoomSpawnPosition()
    {
        return doors[doorIndex].GetSpawnPosition();
    }
    
    public bool MatchTheDoor(Door.Direction direction)
    {
        SetParent(direction);
        
        GameObject doorPos = GetPossibleDoor()?.gameObject;
        if (!doorPos)
            return false;
        doorPos.GetComponent<Door>().SetState(Door.State.door);

        Vector3 doorPosition = doorPos.transform.position;
        Vector3 offSet = doorPosition - transform.position;
        transform.position = transform.position - offSet;
        return true;
    }
    public void ConnectNextDoor()
    {
        doors[doorIndex].SetState(Door.State.door);
    }
        public Door GetnextDoor()
    {
        return doors[doorIndex];
    }

    private Door GetPossibleDoor()
    {
        List<Door> temp = new List<Door>();

        for(int  i = 0; i < doors.Length; i++)
        {
            if (doors[i].direction == PrevDoorDir)
                temp.Add(doors[i]);
        }

        int randomIndex = Random.Range(0, temp.Count);
        if (temp.Count == 0)
            return null;
        return temp[randomIndex];
    }

    public bool CheckOverlapping()
    {
        Collider2D[] room = Physics2D.OverlapBoxAll(transform.position + (Vector3)(boxCollider.offset * transform.localScale),
            boxCollider.size* transform.localScale*1.3f, 0, layerMask);
        Debug.Log($"{this.name}, {room.Length}");
        if (room.Length > 1)
        {
            return false;
        }
        return true;
    }

    public PhysicalRoom GetPreviousRoom()
    {
        return prevRoom;
    }

    public void SetPreviousRoom(PhysicalRoom room)
    {
        prevRoom = room;
    }

    public void DestroyRoom()
    {
        Destroy(gameObject);
    }
}
