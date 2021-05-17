using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    public Transform[]   PossibleStartRooms;
    public Transform[]   PossibleNormalRooms;
    public Transform[]   PossibleHubRooms;
    public Transform[]   PossibleRewardRooms;
    public Transform[]   PossibleShopRooms;
    public Transform[]   PossibleEventRooms;
    public Transform[]   PossibleBossRooms;

    public DungeonName dungeonName;

    private RoomGraph roomGraph;
    private Room _roopPointer;
    private List<int> _exisitingId;

    private int _iterationlimit = 4;

    private void Start()
    {
        roomGraph = GetComponent<RoomGraph>();
        _exisitingId = new List<int>();
        SpawnRooms();
        dungeonName.DisplayName();
    }

    private void SpawnRooms()
    {
        roomGraph.GenerateGraph();
        
        _roopPointer = roomGraph.rooms[1];

        //Create First Room:
        PhysicalRoom StartRoom = CreateStartRoom();

        //DFS visit the grapgh in deapth recursively:
        VisitRoom(_roopPointer, StartRoom);

        //Delete scripts and clean the memory
        roomGraph.DeleteGraph();
        DeleteLogicScript();
    }

    private void VisitRoom(Room room, PhysicalRoom physicalRoom)
    {
        //Get RandomRoom room according to the room type
        Transform NextRoom;

        if (room._type == RoomType.Hub)
            NextRoom = GenerateRandomRoom(PossibleHubRooms);

        else if (room._type == RoomType.Reward)
            NextRoom = GenerateRandomRoom(PossibleRewardRooms);

        else if (room._type == RoomType.Shop)
            NextRoom = GenerateRandomRoom(PossibleShopRooms);

        else if (room._type == RoomType.Event)
            NextRoom = GenerateRandomRoom(PossibleEventRooms);

        else if (room._type == RoomType.Boss)
            NextRoom = PossibleBossRooms[0];

        else 
            NextRoom = GenerateRandomRoom(PossibleNormalRooms);

        //Check the previous room and get empty door
        PhysicalRoom _spawnedRoom = SpawnedRoom(NextRoom, physicalRoom);

        //DFS to go over each original Room and generate Physical copy of it
        for (int i = 0; i < room.count; i++)
        {
            VisitRoom(room._nextRoom[i], _spawnedRoom);
        }
    }

    private Transform GenerateRandomRoom(Transform[] possibilities)
    {
        //Randomize the number:
        int newId = 0;
        bool unique = false;
        while (!unique)
        {
            newId = Random.Range(0, possibilities.Length);

            //Compare new id to the existing one
            //DOES NOT WORK
            int RoomId = possibilities[newId].gameObject.GetComponent<PhysicalRoom>().id;
            unique = true;
            _exisitingId.Add(RoomId);

            /*if (_exisitingId.Find(id => id == RoomId) == 0)
             {
                 unique = true;
                 _exisitingId.Add(RoomId);
             }*/
        }
        return possibilities[newId];
    }
        
    private PhysicalRoom SpawnedRoom(Transform NextRoom, PhysicalRoom physicalRoom)
    {
        PhysicalRoom _spawnedRoom;
        int x = 0;
        while (true)     
        {
            for (int i = 0; i < _iterationlimit; i++)
            {
                Vector3 spawnPoint = physicalRoom.GetNextRoomSpawnPosition();

                //Create new room in its position and assign prev door
                _spawnedRoom = SpawnRoom(NextRoom, spawnPoint, physicalRoom.NextDoorDir);

                if (_spawnedRoom)
                {
                    //Assign the door that should be connected to prev room
                    _spawnedRoom.GenerateNextDoor();
                    physicalRoom.ConnectNextDoor();
                    _spawnedRoom.SetPreviousRoom(physicalRoom);
                    return _spawnedRoom;
                }
                else
                {
                    if (!physicalRoom.GenerateNextDoor())
                        break;
                }
            }

            //If for some reason we cannot place new room, move back till we can place it
            //BACKTRACKING
            x++;
            physicalRoom = physicalRoom.GetPreviousRoom();
        }
    }   

    private PhysicalRoom CreateStartRoom()
    {
        Vector3 spawnPoint = Vector3.zero;
        PhysicalRoom spawnedRoom = SpawnRoom(PossibleStartRooms[Random.Range(0, PossibleStartRooms.Length)], spawnPoint);
        spawnedRoom.GenerateNextDoor();
        return spawnedRoom;
    }

    private PhysicalRoom SpawnRoom(Transform prefab,Vector3 position,Door.Direction direction = Door.Direction.None)
    {
        Transform newRoom = Instantiate(prefab, position, Quaternion.identity);
        if (newRoom)
        {
            PhysicalRoom currentRoom = newRoom.gameObject.GetComponent<PhysicalRoom>();
            //Move the spawned object to match doors if it is not the first room
            if (position!= Vector3.zero)
                if(!currentRoom.MatchTheDoor(direction))
                {
                    currentRoom.DestroyRoom();
                    return null;
                }

            //Check does it overlap with something:
            if(currentRoom.CheckOverlapping())
                return currentRoom;
            else
            {
                currentRoom.DestroyRoom();
                return null;
            }
        }

        else
        {
            return null;
        }
    }

    public void DeleteLogicScript()
    {
        Destroy(this);
    }
}
